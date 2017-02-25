using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Powell.Vehicles.Domain.Migrations._2._0
{
    using Powell.Migrations;

    [Migration(2, 0, 2)]
    public class _2_PopulateYearTable : SqlServerMigrationBase
    {
        public _2_PopulateYearTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Year"
        /// </summary>
        private const string TableName = "Year";

        private static IEnumerable<int> Years
        {
            get
            {
                // Return the numbers explicitly for repeatable performance.
                for (var i = 1970; i <= 2016; i++)
                    yield return i;
            }
        }

        public override void Up()
        {
            var years = string.Join(", ", Years.Select(x => $"(N'1/1/{x}')"));
            Sql($@"INSERT INTO [{SchemaName}].[{TableName}] ([Value]) VALUES {years};");
        }

        public override void Down()
        {
            var years = string.Join(", ", Years.Select(x => $"N'1/1/{x}'"));
            Sql($@"DELETE FROM [{SchemaName}].[{TableName}] WHERE [Value] IN ({years});");
        }
    }
}
