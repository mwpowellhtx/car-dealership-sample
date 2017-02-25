using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Powell.Vehicles.Controllers
{
    using AutoMapper;
    using Managers;
    using Mvc.Controllers;
    using Mvc.Manufacturer;
    using static JsonRequestBehavior;

    public class ManufacturerController : AuthorizedController
    {
        private IManufacturerManager ManufacturerManager { get; }

        private IMapper Mapper { get; }

        // ReSharper disable once SuggestBaseTypeForParameter
        public ManufacturerController(IManufacturerManager manufacturerManager, IManufacturerMapperConfiguration mapperConfiguration)
        {
            ManufacturerManager = manufacturerManager;
            Mapper = mapperConfiguration.CreateMapper();
        }

        public async Task<ActionResult> Index()
        {
            var items = (await ManufacturerManager.GetAllAsync<Manufacturer>())
                .Select(Mapper.Map<Manufacturer, ManufacturerViewModel>)
                .OrderBy(x => x.Name).ToArray();

            return View(items);
        }

        [HttpPost]
        [Route("{name}")]
        public async Task<ActionResult> Add(string name)
        {
            await ManufacturerManager.SaveOrUpdateAsync(new Manufacturer {Name = name});

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var viewModels = (await ManufacturerManager.GetAllAsync<Manufacturer>()).Select(
                Mapper.Map<Manufacturer, ManufacturerViewModel>).ToArray();

            return Json(viewModels, AllowGet);
        }
    }
}
