using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;

namespace Powell.Vehicles.Domain.Migrations._2._0
{
    using Powell.Migrations;
    using static Color;

    [Migration(2, 0, 3)]
    public class _3_PopulatePaintTable : SqlServerMigrationBase
    {
        public _3_PopulatePaintTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Paint"
        /// </summary>
        private const string TableName = "Paint";

        /// <summary>
        /// Gets the default Colors.
        /// </summary>
        /// <see cref="White"/>
        /// <see cref="Red"/>
        /// <see cref="Blue"/>
        /// <see cref="Black"/>
        /// <see cref="Green"/>
        private static IDictionary<string, Color> Colors => new Dictionary<string, Color>
        {
            {White.Name, White},
            {Red.Name, Red},
            {Blue.Name, Blue},
            {Black.Name, Black},
            {Green.Name, Green}
        };

        private static string ToRgbString(Color color)
        {
            // TODO: TBD: could potentially import the domain model into the data layer, but let's try and avoid confusing the assembly references for the time being...
            // TODO: TBD: should we do so, leverage the ToRgbString from the domain layer...
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public override void Up()
        {
            var colors = string.Join(", ", Colors.Select(x => $"(N'{x.Key}', N'{ToRgbString(x.Value)}')"));
            Sql($@"INSERT INTO [{SchemaName}].[{TableName}] ([Name], [Value]) VALUES {colors};");
        }

        public override void Down()
        {
            var colors = string.Join(", ", Colors.Select(x => $"N'{x.Key}'"));
            Sql($@"DELETE FROM [{SchemaName}].[{TableName}] WHERE [Name] IN ({colors});");
        }
    }
}
