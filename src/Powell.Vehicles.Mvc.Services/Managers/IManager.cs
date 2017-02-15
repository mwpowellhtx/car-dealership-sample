using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Powell.Data;

namespace Powell.Vehicles.Managers
{
    using static RepositoryCacheMode;

    public interface IManager
    {
        IEnumerable<T> GetAll<T>(Func<T, bool> predicate = null, RepositoryCacheMode cacheMode = Normal)
            where T : class, new();

        Task<IEnumerable<T>> GetAllAsync<T>(Func<T, bool> predicate = null, RepositoryCacheMode cacheMode = Normal)
            where T : class, new();

        void SaveOrUpdate<T>(params T[] items)
            where T : class, new();

        Task SaveOrUpdateAsync<T>(params T[] items)
            where T : class, new();

        void Delete<T>(params T[] items)
            where T : class, new();

        Task DeleteAsync<T>(params T[] items)
            where T : class, new();
    }
}
