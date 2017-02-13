using System.Data.SqlClient;

namespace Powell.Vehicles.Domain.Migrations._1._1
{
    using Powell.Migrations;

    [Migration(1, 1, 1)]
    public class _1_CreateModelYearColorTable : SqlServerMigrationBase
    {
        public _1_CreateModelYearColorTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "ModelYearColor"
        /// </summary>
        private const string TableName = "ModelYearColor";

        public override void Up()
        {
            /* TODO: TBD: we can pick up one side of the relationship via FK actions; but we must do the other via triggers...
             * this on account of a Sql Server cascading path limitation; consider should all of them be picked up by triggers instead for consistency sake */
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NULL
CREATE TABLE [{SchemaName}].[{TableName}]
(
    [Id] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [DF_{TableName}_Id] DEFAULT NEWSEQUENTIALID()
        CONSTRAINT [PK_{TableName}] PRIMARY KEY
    , [ModelYearId] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [FK_{TableName}_ModelYear] FOREIGN KEY
        REFERENCES [{SchemaName}].[ModelYear] ([Id])
        ON DELETE NO ACTION ON UPDATE NO ACTION
    , [ColorId] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [FK_{TableName}_Paint] FOREIGN KEY
        REFERENCES [{SchemaName}].[Paint] ([Id])
        ON DELETE NO ACTION ON UPDATE NO ACTION
);");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
