using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Powell.Data
{
    using NHibernate;
    using static RepositoryFlushMode;
    using static RepositoryCacheMode;

    internal static class InternalRepositoryExtensionMethods
    {
        private static Lazy<IDictionary<RepositoryCacheMode, CacheMode>> LazyCacheModeMap { get; }

        private static Lazy<IDictionary<RepositoryFlushMode, FlushMode>> LazyFlushModeMap { get; }

        static InternalRepositoryExtensionMethods()
        {
            // One instance where using static shorthand does not work so well.
            LazyCacheModeMap = new Lazy<IDictionary<RepositoryCacheMode, CacheMode>>(
                () => new ConcurrentDictionary<RepositoryCacheMode, CacheMode>(
                    new Dictionary<RepositoryCacheMode, CacheMode>
                    {
                        {Ignore, CacheMode.Ignore},
                        {Put, CacheMode.Put},
                        {Get, CacheMode.Get},
                        {Normal, CacheMode.Normal},
                        {Refresh, CacheMode.Refresh}
                    }));

            LazyFlushModeMap = new Lazy<IDictionary<RepositoryFlushMode, FlushMode>>(
                () => new ConcurrentDictionary<RepositoryFlushMode, FlushMode>(
                    new Dictionary<RepositoryFlushMode, FlushMode>
                    {
                        {Unspecified, FlushMode.Unspecified},
                        {Never, FlushMode.Never},
                        {Commit, FlushMode.Commit},
                        {Auto, FlushMode.Auto},
                        {Always, FlushMode.Always}
                    }));
        }

        internal static CacheMode ToCacheMode(this RepositoryCacheMode mode)
        {
            return LazyCacheModeMap.Value[mode];
        }

        internal static FlushMode ToFlushMode(this RepositoryFlushMode mode)
        {
            return LazyFlushModeMap.Value[mode];
        }
    }
}
