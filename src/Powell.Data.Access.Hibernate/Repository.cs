using System;
using System.Data;
using System.Linq;

namespace Powell.Data
{
    using NHibernate;
    using NHibernate.Linq;
    using static IsolationLevel;
    using static RepositoryFlushMode;

    public class Repository : RepositoryBase<ISession, IRepositorySessionProvider>, IHibernateRepository
    {
        public Repository(IRepositorySessionProvider provider)
            : base(provider)
        {
        }

        private T SessionFunc<T>(Func<ISession, T> func, Action<ISession> onFinally = null)
        {
            onFinally = onFinally ?? delegate { };

            try
            {
                return func(Session);
            }
            // ReSharper disable once RedundantCatchClause
            catch (Exception)
            {
                // TODO: TBD: connnect with logger
                throw;
            }
            finally
            {
                onFinally(Session);
            }
        }

        private void SessionAction(Action<ISession> action, Action<ISession> onFinally = null)
        {
            onFinally = onFinally ?? delegate { };

            try
            {
                action(Session);
            }
            // ReSharper disable once RedundantCatchClause
            catch (Exception)
            {
                // TODO: TBD: connnect with logger
                throw;
            }
            finally
            {
                onFinally(Session);
            }
        }

        public override T Get<T>(object id)
        {
            return SessionFunc(s => s.Get<T>(id));
        }

        public override void Save<T>(params T[] items)
        {
            SessionAction(s =>
            {
                foreach (var item in items)
                    s.Save(item);
            });
        }

        public override void SaveOrUpdate<T>(params T[] items)
        {
            SessionAction(s =>
            {
                foreach (var item in items)
                    s.SaveOrUpdate(item);
            });
        }

        public override void Delete<T>(params T[] items)
        {
            SessionAction(s =>
            {
                foreach (var item in items)
                    s.Delete(item);
            });
        }

        public override IQueryable<T> Query<T>(RepositoryCacheMode cacheMode = DefaultCacheMode)
        {
            // Restore the previous mode afterwards, just in case.
            CacheMode? previousMode = null;

            return SessionFunc(s =>
            {
                previousMode = s.CacheMode;
                s.CacheMode = cacheMode.ToCacheMode();
                return s.Query<T>();
            }, s =>
            {
                // Restore from the previous mode or current one if necessary.
                s.CacheMode = previousMode ?? s.CacheMode;
            });
        }

        public override void Transact(Action<IRepository> work,
            IsolationLevel isolationLevel = ReadCommitted, RepositoryFlushMode flushMode = Commit)
        {
            // Restore the previous mode afterwards, just in case.
            FlushMode? previousMode = null;

            SessionAction(s =>
            {
                previousMode = s.FlushMode;
                s.FlushMode = flushMode.ToFlushMode();

                // Arrange for a disposable transaction.
                using (var transaction = s.BeginTransaction(isolationLevel))
                {
                    // Handle the work within the transaction.
                    work(this);

                    // NHibernate's Transaction Dispose handles the Rollback.
                    transaction.Commit();
                }
            }, s =>
            {
                // Restore from the previous mode or current one if necessary.
                s.FlushMode = previousMode ?? s.FlushMode;
            });
        }

        public override TResult Transact<TResult>(Func<IRepository, TResult> work,
            IsolationLevel isolationLevel = ReadCommitted)
        {
            return SessionFunc(s =>
            {
                TResult result;

                // Arrange for a disposable transaction.
                using (var transaction = s.BeginTransaction(isolationLevel))
                {
                    // Handle the work within the transaction.
                    result = work(this);

                    // NHibernate's Transaction Dispose handles the Rollback.
                    transaction.Commit();
                }

                return result;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                Session.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
