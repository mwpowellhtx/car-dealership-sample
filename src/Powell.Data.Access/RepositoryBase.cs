using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Powell.Data
{
    using static IsolationLevel;
    using static RepositoryCacheMode;
    using static RepositoryFlushMode;

    public abstract class RepositoryBase<TSession, TProvider> : IRepository<TSession>
        where TProvider : IRepositorySessionProvider<TSession>
    {
        protected internal const RepositoryCacheMode DefaultCacheMode = Ignore;

        protected TSession Session { get; }

        protected RepositoryBase(TProvider provider)
        {
            Session = provider.Session;
        }

        public abstract T Get<T>(object id)
            where T : class, new();

        public virtual bool TryGet<T>(object id, out T item)
            where T : class, new()
            => (item = Get<T>(id)) != null;

        public abstract void Save<T>(params T[] items)
            where T : class, new();

        public abstract void SaveOrUpdate<T>(params T[] items)
            where T : class, new();

        public abstract void Delete<T>(params T[] items)
            where T : class, new();

        public abstract IQueryable<T> Query<T>(RepositoryCacheMode cacheMode = DefaultCacheMode)
            where T : class, new();

        public virtual IEnumerable<T> Query<T>(Func<T, bool> predicate,
            RepositoryCacheMode cacheMode = DefaultCacheMode)
            where T : class, new()
            => Query<T>(cacheMode).Where(predicate);

        private static bool DefaultPredicate<T>(T item) => true;

        public virtual bool TryQuery<T>(out IEnumerable<T> result, Func<T, bool> predicate = null,
            RepositoryCacheMode cacheMode = DefaultCacheMode)
            where T : class, new()
        {
            result = Query(predicate ?? DefaultPredicate, cacheMode);
            return result != null && result.Any();
        }

        public abstract void Transact(Action<IRepository> work,
            IsolationLevel isolationLevel = ReadCommitted, RepositoryFlushMode flushMode = Commit);

        public abstract TResult Transact<TResult>(Func<IRepository, TResult> work,
            IsolationLevel isolationLevel = ReadCommitted);

        protected bool IsDisposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            IsDisposed = true;
        }
    }
}
