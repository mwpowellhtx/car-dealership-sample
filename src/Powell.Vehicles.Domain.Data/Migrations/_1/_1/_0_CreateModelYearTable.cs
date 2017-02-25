using System.Data.SqlClient;

namespace Powell.Vehicles.Domain.Migrations._1._1
{
    using Powell.Migrations;

    [Migration(1, 1, 0)]
    public class _0_CreateModelYearTable : SqlServerMigrationBase
    {
        public _0_CreateModelYearTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "ModelYear"
        /// </summary>
        private const string TableName = "ModelYear";

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
    , [ModelId] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [FK_{TableName}_Model] FOREIGN KEY
        REFERENCES [{SchemaName}].[Model] ([Id])
        ON DELETE NO ACTION ON UPDATE NO ACTION
    , [YearId] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [FK_{TableName}_Year] FOREIGN KEY
        REFERENCES [{SchemaName}].[Year] ([Id])
        ON DELETE NO ACTION ON UPDATE NO ACTION
    , [ProducedOn] [DATETIME] NOT NULL
        CONSTRAINT [DF_{TableName}_ProducedOn] DEFAULT GETDATE()
);");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
