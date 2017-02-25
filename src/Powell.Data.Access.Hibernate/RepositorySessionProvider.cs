using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Powell.Data
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using FluentNHibernate.Conventions;
    using NHibernate;
    using static LazyThreadSafetyMode;

    public class RepositorySessionProvider : IRepositorySessionProvider
    {
        private Lazy<ISessionFactory> LazyFactory { get; }

        public ISessionFactory Factory => LazyFactory.Value;

        public ISession Session => Factory.OpenSession();

        public delegate IPersistenceConfigurer CreatePersistenceConfigurer(string connectionString);

        /// <summary>
        /// Sets the Assemblies to scan for Conventions as well as Fluent Mappings. Getter is
        /// private for provider purposes.
        /// </summary>
        public IEnumerable<Assembly> Assemblies { private get; set; }

        /// <summary>
        /// Gets the Conventions that have otherwise been specified apart from the <see cref="Assemblies"/>.
        /// </summary>
        private IConventionPolicy ConventionPolicy { get; }

        private void ConfigureMappings(MappingConfiguration config)
        {
            var m = config.FluentMappings;

            foreach (var assy in Assemblies)
                m.AddFromAssembly(assy);

            {
                var interfaceType = typeof(IConvention);

                var conventionTypes = Assemblies.SelectMany(a => a.GetTypes())
                    .Where(t => interfaceType.IsAssignableFrom(t))
                    .Where(t => t.IsClass && t.IsAbstract && t.IsPublic).ToArray();

                foreach (var type in conventionTypes)
                    m.Conventions.Add(type);
            }

            m.Conventions.ToList().AddRange(ConventionPolicy);
        }

        public RepositorySessionProvider(string connectionString, CreatePersistenceConfigurer configurer
            , IConventionPolicy conventionPolicy)
        {
            // Assemblies must be provided in order for this to work properly.
            ConventionPolicy = conventionPolicy;

            LazyFactory = new Lazy<ISessionFactory>(
                () => Fluently.Configure()
                    .Database(configurer(connectionString))
                    .Mappings(ConfigureMappings)
                    .BuildSessionFactory(), ExecutionAndPublication);
        }
    }
}
