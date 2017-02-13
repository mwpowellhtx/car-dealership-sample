using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._1
{
    /// <summary>
    /// Creates the Permissions Table.
    /// </summary>
    [Migration(3, 1, 2)]
    public class _2_CreatePermissionsTable : SqlServerSecurityMigrationBase
    {
        public _2_CreatePermissionsTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Permission"
        /// </summary>
        private const string TableName = "Permission";

        public override void Up()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NULL
CREATE TABLE [{SchemaName}].[{TableName}]
(
    [Id] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [PK_{TableName}] PRIMARY KEY
        CONSTRAINT [DF_{TableName}_Id] DEFAULT NEWSEQUENTIALID()
    , [CredentialBaseId] [UNIQUEIDENTIFIER] NULL
        CONSTRAINT [FK_{TableName}_Credential] FOREIGN KEY
        REFERENCES [{SchemaName}].[CredentialBase] ([Id])
        ON UPDATE NO ACTION ON DELETE NO ACTION
    , [FeatureId] [UNIQUEIDENTIFIER] NULL
        CONSTRAINT [FK_{TableName}_Feature] FOREIGN KEY
        REFERENCES [{SchemaName}].[Feature] ([Id])
        ON UPDATE NO ACTION ON DELETE NO ACTION
    , [Privilege] [INT] NULL
        CONSTRAINT [DF_{TableName}_Privilege] DEFAULT NULL
    , [ExpiryCreatedOn] [DATETIME] NOT NULL
    , [ExpiryModifiedOn] [DATETIME] NOT NULL
    , [ExpiryExpiresOn] [DATETIME] NULL
        CONSTRAINT [DF_{TableName}_ExpiryExpiresOn] DEFAULT NULL
)");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
