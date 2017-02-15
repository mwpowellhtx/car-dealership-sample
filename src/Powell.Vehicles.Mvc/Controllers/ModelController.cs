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
    }
}
