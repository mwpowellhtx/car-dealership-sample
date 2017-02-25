using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Powell.Vehicles.Domain.Migrations._2._0
{
    using Powell.Migrations;

    [Migration(2, 0, 0)]
    public class _0_PopulateManufacturerTable : SqlServerMigrationBase
    {
        public _0_PopulateManufacturerTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Manufacturer"
        /// </summary>
        private const string TableName = "Manufacturer";

        private static IEnumerable<string> Names
        {
            get
            {
                yield return "Ford";
                yield return "Chevrolet";
                yield return "Dodge";
            }
        }

        public override void Up()
        {
            var names = string.Join(", ", Names.Select(x => $"(N'{x}')"));
            Sql($@"INSERT INTO [{SchemaName}].[{TableName}] ([Name]) VALUES {names};");
        }

        public override void Down()
        {
            var names = string.Join(", ", Names.Select(x => $"N'{x}'"));
            Sql($@"DELETE FROM [{SchemaName}].[{TableName}] WHERE [Name] IN ({names});");
        }
    }
}
