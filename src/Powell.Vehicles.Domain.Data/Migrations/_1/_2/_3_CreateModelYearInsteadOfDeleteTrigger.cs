using System.Data.SqlClient;

namespace Powell.Vehicles.Domain.Migrations._1._2
{
    using Powell.Migrations;

    [Migration(1, 2, 3)]
    public class _3_CreateModelYearInstaceOfDeleteTrigger : SqlServerMigrationBase
    {
        public _3_CreateModelYearInstaceOfDeleteTrigger(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "ModelYear"
        /// </summary>
        private const string TableName = "ModelYear";

        /// <summary>
        /// "TR_ModelYear_InsteadOfDelete"
        /// </summary>
        private const string TriggerName = "TR_ModelYear_InsteadOfDelete";

        public override void Up()
        {
            // This is a tad dangerous with a naked create without check of any sort.
            Sql($@"CREATE TRIGGER [{SchemaName}].[{TriggerName}]
ON [{SchemaName}].[{TableName}]
INSTEAD OF DELETE
AS BEGIN
    SET NOCOUNT ON;
    DELETE FROM [{SchemaName}].[ModelYearColor] WHERE [ModelYearId] IN (SELECT [Id] FROM [DELETED])
    DELETE FROM [{SchemaName}].[Vehicle] WHERE [ModelYearId] IN (SELECT [Id] FROM [DELETED])
END;");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TriggerName}]') IS NOT NULL
DROP TRIGGER [{SchemaName}].[{TriggerName}];");
        }
    }
}
