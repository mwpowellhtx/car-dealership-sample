using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Powell.Data
{
    using static IsolationLevel;
    using static RepositoryCacheMode;
    using static RepositoryFlushMode;

    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Returns the item matching the id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get<T>(object id)
            where T : class, new();

        /// <summary>
        /// Tries to get the item matching the id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        bool TryGet<T>(object id, out T item)
            where T : class, new();

        /// <summary>
        /// Saves the items into the Repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        void Save<T>(params T[] items)
            where T : class, new();

        /// <summary>
        /// Saves or updates the items into the Repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        void SaveOrUpdate<T>(params T[] items)
            where T : class, new();

        /// <summary>
        /// Deletes the items from the Repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        void Delete<T>(params T[] items)
            where T : class, new();

        /// <summary>
        /// Queries the Repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheMode"></param>
        /// <returns></returns>
        IQueryable<T> Query<T>(RepositoryCacheMode cacheMode = Ignore)
            where T : class, new();

        /// <summary>
        /// Queries the Repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="cacheMode"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(Func<T, bool> predicate, RepositoryCacheMode cacheMode = Ignore)
            where T : class, new();

        /// <summary>
        /// Tries to perform the query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="predicate"></param>
        /// <param name="cacheMode"></param>
        /// <returns></returns>
        bool TryQuery<T>(out IEnumerable<T> result, Func<T, bool> predicate = null, RepositoryCacheMode cacheMode = Ignore)
            where T : class, new();

        void Transact(Action<IRepository> work,
            IsolationLevel isolationLevel = ReadCommitted,
            RepositoryFlushMode flushMode = Commit);

        TResult Transact<TResult>(Func<IRepository, TResult> work, IsolationLevel isolationLevel = ReadCommitted);
    }

    public interface IRepository<TSession> : IRepository
    {
    }
}
