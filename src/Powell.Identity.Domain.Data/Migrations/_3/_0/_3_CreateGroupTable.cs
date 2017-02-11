using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._0
{
    /// <summary>
    /// Creates the Security Group Table.
    /// </summary>
    [Migration(3, 0, 3)]
    public class _3_CreateGroupTable : SqlServerSecurityMigrationBase
    {
        public _3_CreateGroupTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Group"
        /// </summary>
        private const string TableName = "Group";

        public override void Up()
        {
            //TODO: TBD: a joining Credential/Membership table might be more appropriate...
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NULL
CREATE TABLE [{SchemaName}].[{TableName}]
(
    [CredentialBaseId] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [PK_{TableName}] PRIMARY KEY
        CONSTRAINT [FK_{TableName}_CredentialBase] FOREIGN KEY
        REFERENCES [{SchemaName}].[CredentialBase] ([Id])
        ON UPDATE CASCADE ON DELETE CASCADE
    , [Id] AS [CredentialBaseId]
    , [Description] [NVARCHAR](MAX) NULL
        CONSTRAINT [DF_{TableName}_Description] DEFAULT NULL
);");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
