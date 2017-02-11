using System.Data.SqlClient;

namespace Powell.Vehicles.Domain.Migrations._1._2
{
    using Powell.Migrations;

    [Migration(1, 2, 0)]
    public class _0_CreateModelInsteadOfDeleteTrigger : SqlServerMigrationBase
    {
        public _0_CreateModelInsteadOfDeleteTrigger(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Model"
        /// </summary>
        private const string TableName = "Model";

        /// <summary>
        /// "TR_Model_InsteadOfDelete"
        /// </summary>
        private const string TriggerName = "TR_Model_InsteadOfDelete";

        public override void Up()
        {
            // This is a tad dangerous with a naked create without check of any sort.
            Sql($@"CREATE TRIGGER [{SchemaName}].[{TriggerName}]
ON [{SchemaName}].[{TableName}]
INSTEAD OF DELETE
AS BEGIN
    SET NOCOUNT ON;
    DELETE FROM [{SchemaName}].[ModelYear] WHERE [ModelId] IN (SELECT [Id] FROM [DELETED])
END;");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TriggerName}]') IS NOT NULL
DROP TRIGGER [{SchemaName}].[{TriggerName}];");
        }
    }
}
