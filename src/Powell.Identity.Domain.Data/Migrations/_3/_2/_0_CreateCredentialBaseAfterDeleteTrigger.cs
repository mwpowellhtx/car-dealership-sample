using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._2
{
    /// <summary>
    /// Re-creates the CredentialBase After Delete Trigger.
    /// </summary>
    [Migration(3, 2, 0)]
    public class _0_CreateCredentialBaseAfterDeleteTrigger : SqlServerSecurityMigrationBase
    {
        public _0_CreateCredentialBaseAfterDeleteTrigger(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "CredentialBase"
        /// </summary>
        private const string TableName = "CredentialBase";

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
        WHERE [GroupId] IN (SELECT [Id] FROM deleted)
            OR [MemberId] IN (SELECT [Id] FROM deleted);

    DELETE FROM [{SchemaName}].[Permission]
        WHERE [{TableName}Id] IN (SELECT [Id] FROM deleted);

    DELETE FROM [{SchemaName}].[User]
        WHERE [{TableName}Id] IN (SELECT [Id] FROM deleted);

    DELETE FROM [{SchemaName}].[Group]
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
