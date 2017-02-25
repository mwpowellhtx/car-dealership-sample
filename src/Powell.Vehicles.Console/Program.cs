using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Cfg.Db;

namespace Powell.Vehicles.Console
{
    using Data;
    using Data.Access.Policies;
    using Domain.Migrations._1._0;
    using Identity;
    using Identity.Domain;
    using Identity.Domain.Migrations._3._0;
    using Migrators;
    using Powell.Domain;
    using static MsSqlConfiguration;

    class Program
    {
        private static string ConnectionString => ConfigurationManager.ConnectionStrings["slnzero"].ConnectionString;

        // TODO: TBD: arrange for this by some sort of "policy" class... such a policy should be close to the module, or perhaps derived from the repository...

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
                MsSql2012.DefaultSchema("dbo").ConnectionString,
                new AccessConventionPolicy()) {Assemblies = GetAssemblies().ToArray()};

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
