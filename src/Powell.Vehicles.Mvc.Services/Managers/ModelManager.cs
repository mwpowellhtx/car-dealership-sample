using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Powell.Vehicles.Managers
{
    using Data;

    public class ModelManager : ControllerManager, IModelManager
    {
        public ModelManager(IHibernateRepository repository)
            : base(repository)
        {
        }

        public virtual Task<IEnumerable<ModelYear>> GetModelYears(Guid modelId)
        {
            return Task.Run(() => GetAll<ModelYear>(x => x.Model.Id == modelId));
        }

        public virtual Task<IEnumerable<Year>> GetAvailableYears(IEnumerable<ModelYear> modelYears)
        {
            // Actually, we may be configuring a Model Year in an existing year and an altogether DIFFERENT Color.
            return Task.Run(() => GetAll<Year>());
        }
    }
}
