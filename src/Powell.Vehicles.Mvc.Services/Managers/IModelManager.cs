using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Powell.Vehicles.Managers
{
    public interface IModelManager : IHibernateManagerBase
    {
        Task<IEnumerable<ModelYear>> GetModelYears(Guid modelId);

        Task<IEnumerable<Year>> GetAvailableYears(IEnumerable<ModelYear> modelYears);
    }
}
