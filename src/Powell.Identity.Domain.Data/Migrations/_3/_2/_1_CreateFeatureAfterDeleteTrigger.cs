using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._2
{
    /// <summary>
    /// Re-creates the Features After Delete Trigger.
    /// </summary>
    [Migration(3, 2, 1)]
    public class _1_CreateFeatureAfterDeleteTrigger : SqlServerSecurityMigrationBase
    {
        public _1_CreateFeatureAfterDeleteTrigger(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Feature"
        /// </summary>
        private const string TableName = "Feature";

        public override void Up()
        {
            Sql($@"CREATE TRIGGER [{SchemaName}].[TR_{TableName}_AfterDelete]
   ON [{SchemaName}].[{TableName}]
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
    DELETE FROM [{SchemaName}].[{TableName}]
        WHERE [ParentId] IN (SELECT [Id] FROM deleted);

    DELETE FROM [{SchemaName}].[Permission]
        WHERE [{TableName}Id] IN (SELECT [Id] FROM deleted);
END");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[TR_{TableName}_AfterDelete]') IS NOT NULL
DROP TRIGGER [{SchemaName}].[TR_{TableName}_AfterDelete];");
        }
    }
}
