using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Powell.Vehicles.Managers;
using Powell.Vehicles.Mvc.ModelYear;

namespace Powell.Vehicles.Controllers
{
    using AutoMapper;
    using Mvc.Controllers;
    using static JsonRequestBehavior;

    public class ModelYearController : AuthorizedController
    {
        private IModelYearManager ModelYearManager { get; }

        private IMapper Mapper { get; }

        // ReSharper disable once SuggestBaseTypeForParameter
        public ModelYearController(IModelYearManager modelYearManager, IModelYearMapperConfiguration mapperConfiguration)
        {
            ModelYearManager = modelYearManager;
            Mapper = mapperConfiguration.CreateMapper();
        }

        public async Task<ActionResult> Index()
        {
            var viewModels = (await ModelYearManager.GetAllAsync<ModelYearColor>())
                .Select(Mapper.Map<ModelYearColor, ModelYearViewModel>)
                .OrderBy(x => x.ManufacturerName)
                .ThenBy(x => x.ModelName)
                .ThenByDescending(x => x.Year)
                .ThenBy(x => x.ColorName).ToArray();

            return View(viewModels);
        }


        public async Task<ActionResult> Add(AddModelYearRequestModel viewModel)
        {
            // First, make sure that the Modelyear itself is accounted for.
            if ((await ModelYearManager.GetAllAsync<ModelYear>(
                    x => x.Year.Value.Year == viewModel.Year
                         && x.Model.Id == viewModel.ModelId)).Any() == false)
            {
                var model = (await ModelYearManager.GetAllAsync<Model>(x => x.Id == viewModel.ModelId)).Single();
                var year = (await ModelYearManager.GetAllAsync<Year>(x => x.Value.Year == viewModel.Year)).Single();
                ModelYearManager.SaveOrUpdateAsync(new ModelYear {Model = model, Year = year}).Wait();
            }

            // Then, cross check with the Colors in which the ModelYear is available.
            var modelYear = (await ModelYearManager.GetAllAsync<ModelYear>(
                x => x.Year.Value.Year == viewModel.Year
                     && x.Model.Id == viewModel.ModelId)).First();

            // ReSharper disable once InvertIf
            if (modelYear.Colors.All(x => x.Id != viewModel.ColorId))
            {
                var color = (await ModelYearManager.GetAllAsync<Paint>(x => x.Id == viewModel.ColorId)).Single();
                ModelYearManager.SaveOrUpdateAsync(new ModelYearColor {ModelYear = modelYear, Color = color}).Wait();
            }

            // Then simply relist the model years.
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Get(RequestModelYearsRequestModel requestModel)
        {
            var model = (await ModelYearManager.GetAllAsync<Model>(x => x.Id == requestModel.ModelId)).Single();

            var availableYears = (await ModelYearManager.GetAvailableYears(model.ModelYears)).ToArray();

            var responseModel = Mapper.Map<Model, AvailableYearsResponseModel>(model
                , opts => opts.AfterMap(
                    (s, d) => d.Years = availableYears.Select(x => x.Value.Year).OrderByDescending(x => x).ToList()
                )
            );

            return Json(responseModel, AllowGet);
        }
    }
}
