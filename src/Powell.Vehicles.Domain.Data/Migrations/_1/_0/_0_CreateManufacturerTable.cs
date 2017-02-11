using System.Data.SqlClient;

namespace Powell.Vehicles.Domain.Migrations._1._0
{
    using Powell.Migrations;

    [Migration(1, 0, 0)]
    public class _0_CreateManufacturerTable : SqlServerMigrationBase
    {
        public _0_CreateManufacturerTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Manufacturer"
        /// </summary>
        private const string TableName = "Manufacturer";

        public override void Up()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NULL
CREATE TABLE [{SchemaName}].[{TableName}]
(
    [Id] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [DF_{TableName}_Id] DEFAULT NEWSEQUENTIALID()
        CONSTRAINT [PK_{TableName}] PRIMARY KEY
    , [Name] [NVARCHAR](MAX) NOT NULL
);");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
