using System.Collections.Generic;
using System.Reflection;

namespace Powell.Data
{
    using NHibernate;

    public interface IRepositorySessionProvider : IRepositorySessionProvider<ISession>
    {
        /// <summary>
        /// Sets the Assemblies to scan for Conventions as well as Fluent Mappings.
        /// </summary>
        IEnumerable<Assembly> Assemblies { set; }
    }
}
