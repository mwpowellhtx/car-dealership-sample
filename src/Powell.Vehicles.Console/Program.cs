using System.Configuration;

namespace Powell.Vehicles.Console
{
    using Migrators;

    class Program
    {
        private static string ConnectionString => ConfigurationManager.ConnectionStrings["slnzero"].ConnectionString;

        static void Main(string[] args)
        {
            // Verify that at least the database can be migrated downwards and upwards.
            using (var migrator = new SqlServerMigrator(ConnectionString
                , typeof(SqlServerMigrator).Assembly))
            {
                migrator.Down().Up();
            }
        }
    }
}
