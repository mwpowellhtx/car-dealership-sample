using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._2
{
    /// <summary>
    /// Re-creates the Users After Delete Trigger.
    /// </summary>
    [Migration(3, 2, 2)]
    public class _2_CreateUserAfterDeleteTrigger : SqlServerSecurityMigrationBase
    {
        public _2_CreateUserAfterDeleteTrigger(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "User"
        /// </summary>
        private const string TableName = "User";

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
    DELETE FROM [{SchemaName}].[LoginInfo]
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
