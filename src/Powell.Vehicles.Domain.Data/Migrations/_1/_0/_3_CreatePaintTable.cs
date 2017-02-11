using System.Data.SqlClient;
using System.Drawing;

namespace Powell.Vehicles.Domain.Migrations._1._0
{
    using Powell.Migrations;

    [Migration(1, 0, 3)]
    public class _3_CreatePaintTable : SqlServerMigrationBase
    {
        public _3_CreatePaintTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Paint"
        /// </summary>
        private const string TableName = "Paint";

        /// <summary>
        /// Gets the DefaultValue: <see cref="Color.White"/>
        /// </summary>
        private static Color DefaultValue => Color.White;

        public override void Up()
        {
            // TODO: TBD: Fairly certain 7 characters is just enough to deal with Paint Value.
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NULL
CREATE TABLE [{SchemaName}].[{TableName}]
(
    [Id] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [DF_{TableName}_Id] DEFAULT NEWSEQUENTIALID()
        CONSTRAINT [PK_{TableName}] PRIMARY KEY
    , [Name] [NVARCHAR](MAX) NOT NULL
    , [Value] [NVARCHAR](7) NOT NULL
CONSTRAINT [DF_{TableName}_Value] DEFAULT N'{DefaultValue}'
);");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
