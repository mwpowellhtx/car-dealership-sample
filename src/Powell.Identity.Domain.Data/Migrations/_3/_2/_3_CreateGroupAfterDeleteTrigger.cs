using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._2
{
    /// <summary>
    /// Re-creates the Users After Delete Trigger.
    /// </summary>
    [Migration(3, 2, 3)]
    public class _3_CreateGroupAfterDeleteTrigger : SqlServerSecurityMigrationBase
    {
        public _3_CreateGroupAfterDeleteTrigger(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Group"
        /// </summary>
        private const string TableName = "Group";

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
    DELETE FROM [{SchemaName}].[Membership]
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
