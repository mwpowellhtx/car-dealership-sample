using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Powell.Data;

namespace Powell.Vehicles.Managers
{
    using static IsolationLevel;
    using static RepositoryFlushMode;
    using static RepositoryCacheMode;

    public abstract class ManagerBase<TRepository> : IManager
        where TRepository : class, IHibernateRepository
    {
        protected TRepository Repository { get; }

        protected ManagerBase(TRepository repository)
        {
            Repository = repository;
        }

        protected virtual void Transact(Action<IRepository> work
            , IsolationLevel isolationLevel = ReadCommitted
            , RepositoryFlushMode flushMode = Commit)
        {
            Repository.Transact(work, isolationLevel);
        }

        protected virtual TResult Transact<TResult>(Func<IRepository, TResult> work
            , IsolationLevel isolationLevel = ReadCommitted)
        {
            return Repository.Transact(work, isolationLevel);
        }

        public virtual IEnumerable<T> GetAll<T>(Func<T, bool> predicate = null
            , RepositoryCacheMode cacheMode = Normal)
            where T : class, new()
        {
            return Transact(r => r.Query(predicate, cacheMode).ToArray());
        }

        public virtual Task<IEnumerable<T>> GetAllAsync<T>(Func<T, bool> predicate = null,
            RepositoryCacheMode cacheMode = Normal)
            where T : class, new()
        {
            return Task.Run(() => GetAll(predicate, cacheMode));
        }

        public virtual void SaveOrUpdate<T>(params T[] items)
            where T : class, new()
        {
            Transact(r => r.SaveOrUpdate(items));
        }


        public virtual Task SaveOrUpdateAsync<T>(params T[] items)
            where T : class, new()
        {
            return Task.Run(() => SaveOrUpdate(items));
        }

        public virtual void Delete<T>(params T[] items)
            where T : class, new()
        {
            Transact(r => r.Delete(items));
        }

        public virtual Task DeleteAsync<T>(params T[] items)
            where T : class, new()
        {
            return Task.Run(() => Delete(items));
        }
    }
}
