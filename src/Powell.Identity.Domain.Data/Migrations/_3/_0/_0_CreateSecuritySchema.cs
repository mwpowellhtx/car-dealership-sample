using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._0
{
    [Migration(3, 0, 0)]
    public class _0_CreateSecuritySchema : SqlServerSecurityMigrationBase
    {
        public _0_CreateSecuritySchema(SqlConnection connection)
            : base(connection)
        {
        }

        public override void Up()
        {
            // Neither OBJECT_ID nor SCHEMA_ID seem to do the trick here.
            Sql($@"IF NOT EXISTS (SELECT * FROM [SYS].[SCHEMAS] WHERE [name]=N'{SchemaName}')
EXEC sp_sqlexec N'CREATE SCHEMA [{SchemaName}]';");
        }

        public override void Down()
        {
            // Ditto OBJECT_ID and SCHEMA_ID.
            Sql($@"IF EXISTS (SELECT * FROM [SYS].[SCHEMAS] WHERE [name]=N'{SchemaName}')
EXEC sp_sqlexec N'DROP SCHEMA [{SchemaName}]';");
        }
    }
}
