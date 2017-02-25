using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._0
{
    /// <summary>
    /// Creates the CredentialBase Table.
    /// </summary>
    [Migration(3, 0, 1)]
    public class _1_CreateCredentialBaseTable : SqlServerSecurityMigrationBase
    {
        public _1_CreateCredentialBaseTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "CredentialBase"
        /// </summary>
        private const string TableName = "CredentialBase";

        public override void Up()
        {
            // TODO: TBD: a joining Credential/Membership table might be more appropriate...
            // TODO: TBD: could make a proper database type complete with CASE expected values of CredentialType
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NULL
CREATE TABLE [{SchemaName}].[{TableName}]
(
    [Id] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [PK_{TableName}_Id] PRIMARY KEY
        CONSTRAINT [DF_{TableName}_Id] DEFAULT NEWSEQUENTIALID()
    , [Name] [NVARCHAR](128) NOT NULL
    , [ExpiryCreatedOn] [DATETIME] NOT NULL
    , [ExpiryModifiedOn] [DATETIME] NOT NULL
    , [ExpiryExpiresOn] [DATETIME] NULL
        CONSTRAINT [DF_{TableName}_ExpiryExpiresOn] DEFAULT NULL
    , CONSTRAINT [CK_{TableName}_Name] CHECK (LEN([Name]) > 0)
    , CONSTRAINT [UQ_{TableName}_Name] UNIQUE ([Name])
);");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
