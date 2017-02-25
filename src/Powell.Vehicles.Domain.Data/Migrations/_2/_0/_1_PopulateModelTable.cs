using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Powell.Vehicles.Domain.Migrations._2._0
{
    using Powell.Migrations;

    [Migration(2, 0, 1)]
    public class _1_PopulateModelTable : SqlServerMigrationBase
    {
        public _1_PopulateModelTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Model"
        /// </summary>
        private const string TableName = "Model";

        private static IDictionary<string, string> Models => new Dictionary<string, string>
        {
            {"Ford", "Mustang"},
            {"Chevrolet", "Corvette"},
            {"Dodge", "Charger"}
        };

        public override void Up()
        {
            var models = string.Join(", ", Models.Select(x => $"((SELECT [Id] FROM [{SchemaName}].[Manufacturer] WHERE [Name]=N'{x.Key}'), N'{x.Value}')"));
            Sql($@"INSERT INTO [{SchemaName}].[{TableName}] ([MakeId], [Name]) VALUES {models};");
        }

        public override void Down()
        {
            var names = string.Join(", ", Models.Values.Select(x => $"N'{x}'"));
            Sql($@"DELETE FROM [{SchemaName}].[{TableName}] WHERE [Name] IN ({names});");
        }
    }
}
