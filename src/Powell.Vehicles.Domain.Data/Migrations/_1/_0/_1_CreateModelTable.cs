using System.Data.SqlClient;

namespace Powell.Vehicles.Domain.Migrations._1._0
{
    using Powell.Migrations;

    [Migration(1, 0, 1)]
    public class _1_CreateModelTable : SqlServerMigrationBase
    {
        public _1_CreateModelTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Model"
        /// </summary>
        private const string TableName = "Model";

        public override void Up()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NULL
CREATE TABLE [{SchemaName}].[{TableName}]
(
    [Id] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [DF_{TableName}_Id] DEFAULT NEWSEQUENTIALID()
        CONSTRAINT [PK_{TableName}] PRIMARY KEY
    , [MakeId] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [FK_{TableName}_Manufacturer] FOREIGN KEY
        REFERENCES [dbo].[Manufacturer] ([Id])
        ON DELETE NO ACTION ON UPDATE NO ACTION
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
