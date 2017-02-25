using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Powell.Vehicles.Controllers
{
    using AutoMapper;
    using Managers;
    using Mvc.Controllers;
    using Mvc.Paint;
    using static JsonRequestBehavior;

    public class PaintController : AuthorizedController
    {
        private IPaintManager PaintManager { get; }

        private IMapper Mapper { get; }

        // ReSharper disable once SuggestBaseTypeForParameter
        public PaintController(IPaintManager paintManager, IPaintMapperConfiguration mapperConfiguration)
        {
            PaintManager = paintManager;
            Mapper = mapperConfiguration.CreateMapper();
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var colors = (await PaintManager.GetAllAsync<Paint>())
                .Select(Mapper.Map<Paint, PaintResponseModel>)
                .OrderBy(x => x.Name).ToArray();

            return Json(colors, AllowGet);
        }
    }
}
