using System.Configuration;

namespace Powell.Vehicles.Console
{
    using Domain.Migrations._1._0;
    using Identity.Domain.Migrations._3._0;
    using Migrators;

    class Program
    {
        private static string ConnectionString => ConfigurationManager.ConnectionStrings["slnzero"].ConnectionString;

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
        }
    }
}
