using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Powell.Vehicles.Controllers
{
    using AutoMapper;
    using Managers;
    using Mvc.Controllers;
    using Mvc.Vehicle;

    public class VehicleController : AuthorizedController
    {
        private IVehicleManager VehicleManager { get; }

        private IMapper Mapper { get; }

        // ReSharper disable once SuggestBaseTypeForParameter
        public VehicleController(IVehicleManager vehicleManager, IVehicleMapperConfiguration mapperConfiguration)
        {
            VehicleManager = vehicleManager;
            Mapper = mapperConfiguration.CreateMapper();
        }

        private VehicleViewModel InformClosestModelYearColor(VehicleViewModel viewModel)
        {
            var choices = VehicleManager.GetAll<ModelYearColor>(
                    x => x.ModelYear.Model.Id == viewModel.ModelId
                         && x.ModelYear.Model.Make.Id == viewModel.ManufacturerId)
                .ToArray();

            /* Follow the punctionation here. We're either getting the choice, or the first (or
             * default), and Id, if it is available, or the Empty value when all else fails. If
             * we've gotten this far, that should be a rare, if ever, corner case. */

            var choice = choices.FirstOrDefault(x => x.Color.Id == viewModel.ColorId);
            viewModel.ModelYearColorId = (choice ?? choices.FirstOrDefault())?.Id ?? Guid.Empty;

            return viewModel;
        }

        public async Task<ActionResult> Index()
        {
            var viewModels = (await VehicleManager.GetAllAsync<Vehicle>())
                    .Select(Mapper.Map<Vehicle, VehicleViewModel>)
                    .Select(InformClosestModelYearColor)
                    .OrderBy(x => x.ManufacturerName)
                    .ThenBy(x => x.ModelName)
                    .ThenByDescending(x => x.Year)
                    .ThenBy(x => x.ColorName)
                    .ThenBy(x => x.Mileage)
                ;

            return View(viewModels);
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddVehicleRequestModel requestModel)
        {
            // Remember this Controller operates from ModelYearColor perspective for starters.
            var modelYearColor = (await VehicleManager.GetAllAsync<ModelYearColor>(
                x => x.Id == requestModel.ModelYearColorId)).SingleOrDefault();

            var color = (await VehicleManager.GetAllAsync<Paint>(
                x => x.Id == requestModel.ColorId)).SingleOrDefault();

            // ReSharper disable once InvertIf
            if (!(modelYearColor == null || color == null))
            {
                var vehicle = Mapper.Map(requestModel, new Vehicle {ModelYear = modelYearColor.ModelYear, Color = color});
                // Save the mapped Vehicle request.
                VehicleManager.SaveOrUpdateAsync(vehicle).Wait();
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Update(UpdateVehicleRequestModel requestModel)
        {
            var vehicle = (await VehicleManager.GetAllAsync<Vehicle>(
                x => x.Id == requestModel.VehicleId)).SingleOrDefault();

            // ReSharper disable once InvertIf
            if (vehicle != null)
            {
                var modelYearColor = (await VehicleManager.GetAllAsync<ModelYearColor>(
                    x => x.Id == requestModel.ModelYearColorId)).SingleOrDefault();

                var color = (await VehicleManager.GetAllAsync<Paint>(
                    x => x.Id == requestModel.ColorId)).SingleOrDefault();

                if (!(modelYearColor == null || color == null))
                {
                    // Map the request to the domain model. Translate the ModelYear and Color as well.
                    Mapper.Map(requestModel, vehicle
                        , opts => opts.AfterMap((s, d) =>
                        {
                            d.ModelYear = modelYearColor.ModelYear;
                            d.Color = color;
                        }));

                    VehicleManager.SaveOrUpdateAsync(vehicle).Wait();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(DeleteVehicleRequestModel requestModel)
        {
            // There should be the One but we could anticipate more downstream from here.
            var vehicles = (await VehicleManager.GetAllAsync<Vehicle>(
                x => x.Id == requestModel.VehicleId)).ToArray();

            if (vehicles.Any())
            {
                VehicleManager.DeleteAsync(vehicles).Wait();
            }

            return RedirectToAction("Index");
        }
    }
}
