using System.Data.SqlClient;

namespace Powell.Vehicles.Domain.Migrations._1._1
{
    using Powell.Migrations;

    [Migration(1, 1, 2)]
    public class _2_CreateVehicleTable : SqlServerMigrationBase
    {
        public _2_CreateVehicleTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Vehicle"
        /// </summary>
        private const string TableName = "Vehicle";

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
    , [PaintId] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [FK_{TableName}_Paint] FOREIGN KEY
        REFERENCES [{SchemaName}].[Paint] ([Id])
        ON DELETE NO ACTION ON UPDATE NO ACTION
    , [Mileage] [FLOAT] NOT NULL
        CONSTRAINT [CK_{TableName}_Mileage] CHECK ([Mileage] >= 0)
    , [Description] [NVARCHAR](MAX) NOT NULL
        CONSTRAINT [DF_{TableName}_Description] DEFAULT N''
);");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
