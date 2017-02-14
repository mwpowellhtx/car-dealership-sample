using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Cfg.Db;
using Powell.Data;

namespace Powell.Vehicles.Modules
{
    using Autofac;
    using Autofac.Core;
    using Data.Access.Policies;
    using Domain;
    using Identity;
    using static RepositorySessionProvider;
    using static MsSqlConfiguration;

    public class DataAccessModule : Module
    {
        private static IEnumerable<Parameter> GetRepositorySessionProviderParameters()
        {
            {
                var connectionString = ConfigurationManager.ConnectionStrings["slnzero"].ConnectionString;
                yield return new NamedParameter(nameof(connectionString), connectionString);
            }

            {
                CreatePersistenceConfigurer configurer = MsSql2012.DefaultSchema("dbo").ConnectionString;
                yield return new NamedParameter(nameof(configurer), configurer);
            }
        }

        private static IEnumerable<Assembly> GetRepositorySessionProviderAssemblies()
        {
            yield return typeof(DomainObjectMap<>).Assembly;
            yield return typeof(VehicleMap).Assembly;
            yield return typeof(UserSubclassMap).Assembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AccessConventionPolicy>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<RepositorySessionProvider>()
                .AsImplementedInterfaces()
                .WithParameters(GetRepositorySessionProviderParameters())
                .OnActivating(args => args.Instance.Assemblies = GetRepositorySessionProviderAssemblies().ToArray())
                .SingleInstance();
        }
    }
}
