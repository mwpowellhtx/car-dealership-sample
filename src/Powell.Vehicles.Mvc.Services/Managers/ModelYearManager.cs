using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Powell.Vehicles.Managers
{
    using Data;

    public class ModelYearManager : ControllerManager, IModelYearManager
    {
        public ModelYearManager(IHibernateRepository repository)
            : base(repository)
        {
        }

        public virtual Task<IEnumerable<ModelYear>> GetModelYears(Guid modelId)
        {
            return Task.Run(() => GetAll<ModelYear>(x => x.Model.Id == modelId));
        }

        public virtual Task<IEnumerable<Year>> GetAvailableYears(IEnumerable<ModelYear> modelYears)
        {
            return Task.Run(() => GetAll<Year>().Where(x => modelYears.All(y => y.Year.Value != x.Value)));
        }
    }
}
