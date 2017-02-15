using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Powell.Vehicles.Controllers
{
    using AutoMapper;
    using Managers;
    using Mvc.Controllers;
    using Mvc.Model;
    using static JsonRequestBehavior;

    public class ModelController : AuthorizedController
    {
        private IModelManager ModelManager { get; }

        private IMapper Mapper { get; }

        // ReSharper disable once SuggestBaseTypeForParameter
        public ModelController(IModelManager modelManager, IModelMapperConfiguration mapperConfiguration)
        {
            Mapper = mapperConfiguration.CreateMapper();
            ModelManager = modelManager;
        }

        [HttpGet]
        public async Task<ActionResult> Get(RequestModelsRequestModel requestModel)
        {
            var models = (await ModelManager.GetAllAsync<Model>(
                    x => requestModel.ManufacturerId == x.Make.Id))
                .Select(Mapper.Map<Model, ModelResponseModel>).ToArray();

            return Json(models, AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetAvailableYears(AvailableYearsRequestModel requestModel)
        {
            var model = (await ModelManager.GetAllAsync<Model>(x => x.Id== requestModel.ModelId)).Single();

            var availableYears = (await ModelManager.GetAvailableYears(model.ModelYears)).ToArray();

            var responseModel = Mapper.Map<Model, AvailableYearsResponseModel>(model
                , opts => opts.AfterMap(
                    (s, d) => d.Years = availableYears.Select(x => x.Value.Year)
                        .OrderByDescending(x => x).ToList()
                )
            );

            return Json(responseModel, AllowGet);
        }

    }
}
