using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Powell.Vehicles.Controllers
{
    using AutoMapper;
    using Managers;
    using Mvc.Controllers;
    using Mvc.Year;

    public class YearController : AuthorizedController
    {
        private IYearManager YearManager { get; }

        private IMapper YearMapper { get; }

        // ReSharper disable once SuggestBaseTypeForParameter
        public YearController(IYearManager yearManager, IYearMapperConfiguration mapperConfiguration)
        {
            YearManager = yearManager;
            YearMapper = mapperConfiguration.CreateMapper();
        }

        public async Task<ActionResult> Index()
        {
            var viewModels = (await YearManager.GetAllAsync<Year>())
                .Select(YearMapper.Map<Year, YearViewModel>)
                .OrderByDescending(x => x.Year).ToArray();

            return View(viewModels);
        }

        [HttpPost]
        [Route("{year?}")]
        public async Task<ActionResult> Add(int? year = null)
        {
            if (year == null) await YearManager.AddAsync();
            else await YearManager.AddAsync(year.Value);

            return RedirectToAction("Index");
        }
    }
}
