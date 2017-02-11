using System.Data.SqlClient;

namespace Powell.Vehicles.Domain.Migrations._1._0
{
    using Powell.Migrations;

    [Migration(1, 0, 2)]
    public class _2_CreateYearTable : SqlServerMigrationBase
    {
        public _2_CreateYearTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Year"
        /// </summary>
        private const string TableName = "Year";

        public override void Up()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NULL
CREATE TABLE [{SchemaName}].[{TableName}]
(
    [Id] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [DF_{TableName}_Id] DEFAULT NEWSEQUENTIALID()
        CONSTRAINT [PK_{TableName}] PRIMARY KEY
    , [Value] [DATETIME] NOT NULL
);");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
