using System.Data.SqlClient;

namespace Powell.Vehicles.Domain.Migrations._1._2
{
    using Powell.Migrations;

    [Migration(1, 2, 4)]
    public class _4_CreatePaintInstaceOfDeleteTrigger : SqlServerMigrationBase
    {
        public _4_CreatePaintInstaceOfDeleteTrigger(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Paint"
        /// </summary>
        private const string TableName = "Paint";

        /// <summary>
        /// "TR_Paint_InsteadOfDelete"
        /// </summary>
        private const string TriggerName = "TR_Paint_InsteadOfDelete";

        public override void Up()
        {
            // This is a tad dangerous with a naked create without check of any sort.
            Sql($@"CREATE TRIGGER [{SchemaName}].[{TriggerName}]
ON [{SchemaName}].[{TableName}]
INSTEAD OF DELETE
AS BEGIN
    SET NOCOUNT ON;

    DELETE FROM [{SchemaName}].[ModelYearColor]
        WHERE [ColorId] IN (SELECT [Id] FROM [DELETED])

    DELETE FROM [{SchemaName}].[Vehicle]
        WHERE [ColorId] IN (SELECT [Id] FROM [DELETED])

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
