using System.Data.SqlClient;

namespace Powell.Vehicles.Domain.Migrations._1._2
{
    using Powell.Migrations;

    [Migration(1, 2, 0)]
    public class _0_CreateManufacturerInsteadOfDeleteTrigger : SqlServerMigrationBase
    {
        public _0_CreateManufacturerInsteadOfDeleteTrigger(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Manufacturer"
        /// </summary>
        private const string TableName = "Manufacturer";

        /// <summary>
        /// "TR_Manufacturer_InsteadOfDelete"
        /// </summary>
        private const string TriggerName = "TR_Manufacturer_InsteadOfDelete";

        public override void Up()
        {
            // This is a tad dangerous with a naked create without check of any sort.
            Sql($@"CREATE TRIGGER [{SchemaName}].[{TriggerName}]
ON [{SchemaName}].[{TableName}]
INSTEAD OF DELETE
AS BEGIN
    SET NOCOUNT ON;

    DELETE FROM [{SchemaName}].[Model]
        WHERE [MakeId] IN (SELECT [Id] FROM [DELETED])

    DELETE FROM [{SchemaName}].[{TableName}]
        WHERE [Id] IN (SELECT [Id] FROM [DELETED])
END;");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TriggerName}]') IS NOT NULL
DROP TRIGGER [{SchemaName}].[{TriggerName}];");
        }
    }
}
