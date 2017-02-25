using System;

namespace Powell.Data
{
    /// <summary>
    /// This is a parallel enum to the NHibernate CacheMode. We do this in order to not incur
    /// a dependency earlier than we need.
    /// </summary>
    [Serializable]
    [Flags]
    public enum RepositoryCacheMode
    {
        /// <summary>
        /// 0
        /// </summary>
        Ignore = 0,

        /// <summary>
        /// 1
        /// </summary>
        Put = 1 << 0,

        /// <summary>
        /// 2
        /// </summary>
        Get = 1 << 1,

        /// <summary>
        /// 3
        /// </summary>
        Normal = Put | Get,

        /// <summary>
        /// 5
        /// </summary>
        Refresh = (1 << 2) | Put
    }
}