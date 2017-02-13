using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;

namespace Powell.Vehicles.Console
{
    using Data;
    using Domain.Migrations._1._0;
    using Identity;
    using Identity.Domain;
    using Identity.Domain.Migrations._3._0;
    using Migrators;
    using Powell.Domain;

    class Program
    {
        private static string ConnectionString => ConfigurationManager.ConnectionStrings["slnzero"].ConnectionString;

        // TODO: TBD: arrange for this by some sort of "policy" class... such a policy should be close to the module, or perhaps derived from the repository...

        private interface IExperimentalConventionPolicy : IConventionPolicy
        {
        }

        private class ExperimentalConventionPolicy : ConventionPolicyBase, IExperimentalConventionPolicy
        {
            private static IEnumerable<IConvention> GetConventions()
            {
                yield return Table.Is(x => x.EntityType.Name);

                // TODO: TBD: does not seem to be taking? having to specify Id column?
                yield return ConventionBuilder.Id.Always(x => x.Column("Id"));

                yield return ConventionBuilder.HasMany.Always(x => x.Cascade.AllDeleteOrphan());

                yield return ConventionBuilder.HasMany.Always(x => x.LazyLoad());

                yield return ConventionBuilder.HasMany.Always(x => x.Inverse());

                yield return ConventionBuilder.HasMany.Always(x => x.AsBag());

                yield return ConventionBuilder.Reference.Always(x => x.LazyLoad());

                yield return ConventionBuilder.Reference.Always(x => x.Cascade.All());

                //// TODO: TBD: not strong enough:
                //yield return ForeignKey.EndsWith("Id");

                // TODO: TBD: does not seem to be taking? having to specify each reference column name?
                yield return ForeignKey.Format((p, t) => (p == null ? t.Name : p.Name) + "Id");

                // TODO: ditto Id Alwyas Id?
                yield return PrimaryKey.Name.Is(x => "Id");

                yield return DefaultCascade.All();

                yield return DefaultLazy.Always();
            }

            internal ExperimentalConventionPolicy()
                : base(GetConventions().ToArray())
            {
            }
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(DomainObjectMap<>).Assembly;
            yield return typeof(VehicleMap).Assembly;
            yield return typeof(UserSubclassMap).Assembly;
        }

        private static void VerifyMappings()
        {
            // At minimum these should work without exception as a kind of smoke test.
            var provider = new RepositorySessionProvider(ConnectionString,
                MsSqlConfiguration.MsSql2012.DefaultSchema("dbo").ConnectionString,
                new ExperimentalConventionPolicy()) {Assemblies = GetAssemblies().ToArray()};

            // Creating the Repository rolls up the ISession request; emulating what would happen under DI conditions.
            using (var repository = new Repository(provider))
            {
                var r = repository;

                var users = r.Query<User>().ToList();
                Debug.Assert(users != null);

                var claims = r.Query<Claim>().ToList();
                Debug.Assert(claims != null);

                var loginInfo = r.Query<LoginInfo>().ToList();
                Debug.Assert(loginInfo != null);

                var groups = r.Query<Group>().ToList();
                Debug.Assert(groups != null);

                var membership = r.Query<Membership>().ToList();
                Debug.Assert(membership != null);

                var features = r.Query<Feature>().ToList();
                Debug.Assert(features != null);

                var permissions = r.Query<Permission>().ToList();
                Debug.Assert(permissions != null);
            }

            using (var repository = new Repository(provider))
            {
                var r = repository;

                var manufacturers = r.Query<Manufacturer>().ToList();
                Debug.Assert(manufacturers != null);

                var models = r.Query<Model>().ToList();
                Debug.Assert(models != null);

                var modelYears = r.Query<ModelYear>().ToList();
                Debug.Assert(modelYears != null);

                var modelYearColors = r.Query<ModelYearColor>().ToList();
                Debug.Assert(modelYearColors != null);

                var paints = r.Query<Paint>().ToList();
                Debug.Assert(paints != null);

                var vehicles = r.Query<Vehicle>().ToList();
                Debug.Assert(vehicles != null);

                var years = r.Query<Year>().ToList();
                Debug.Assert(years != null);
            }
        }

        static void Main(string[] args)
        {
            // Verify that at least the database can be migrated downwards and upwards.
            using (var migrator = new SqlServerMigrator(ConnectionString
                    , typeof(SqlServerMigrator).Assembly
                    , typeof(_0_CreateManufacturerTable).Assembly
                    , typeof(_0_CreateSecuritySchema).Assembly
                )
            )
            {
                migrator.Down().Up();
            }

            VerifyMappings();
        }
    }
}
