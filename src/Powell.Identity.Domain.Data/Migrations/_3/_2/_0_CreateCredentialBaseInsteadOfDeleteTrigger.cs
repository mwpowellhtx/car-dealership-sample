using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._2
{
    /// <summary>
    /// Re-creates the CredentialBase Instead Of Delete Trigger.
    /// </summary>
    [Migration(3, 2, 0)]
    public class _0_CreateCredentialBaseInsteadOfDeleteTrigger : SqlServerSecurityMigrationBase
    {
        public _0_CreateCredentialBaseInsteadOfDeleteTrigger(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "CredentialBase"
        /// </summary>
        private const string TableName = "CredentialBase";

        public override void Up()
        {
            Sql($@"CREATE TRIGGER [{SchemaName}].[TR_{TableName}_InsteadOfDelete]
   ON [{SchemaName}].[{TableName}]
   INSTEAD OF DELETE
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

    -- Rememer to also delete itself when INSTEAD OF DELETE
    DELETE FROM [Security].[CredentialBase]
        WHERE [Id] IN (SELECT [Id] FROM deleted);
END");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[TR_{TableName}_InsteadOfDelete]') IS NOT NULL
DROP TRIGGER [{SchemaName}].[TR_{TableName}_InsteadOfDelete];");
        }
    }
}
