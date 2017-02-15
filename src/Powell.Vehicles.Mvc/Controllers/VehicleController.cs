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

        public async Task<ActionResult> Index()
        {
            var viewModels = (await VehicleManager.GetAllAsync<Vehicle>())
                    .Select(Mapper.Map<Vehicle, VehicleViewModel>)
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
