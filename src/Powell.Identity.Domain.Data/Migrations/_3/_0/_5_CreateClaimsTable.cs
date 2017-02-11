using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._0
{
    /// <summary>
    /// Creates the Security Claims Table.
    /// </summary>
    [Migration(3, 0, 5)]
    public class _5_CreateClaimsTable : SqlServerSecurityMigrationBase
    {
        public _5_CreateClaimsTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Claim"
        /// </summary>
        private const string TableName = "Claim";

        public override void Up()
        {
            //TODO: I don't know how long TypeUri, Value, or ValueType should be: 511 is probably plenty
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NULL
CREATE TABLE [{SchemaName}].[{TableName}]
(
    [Id] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [PK_{TableName}] PRIMARY KEY
        CONSTRAINT [DF_{TableName}_Id] DEFAULT NEWSEQUENTIALID()
    , [UserId] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [FK_{TableName}_User] FOREIGN KEY
        REFERENCES [{SchemaName}].[User] ([CredentialBaseId])
        ON UPDATE NO ACTION ON DELETE NO ACTION
    , [TypeUri] [VARCHAR](511) NOT NULL
    , [Value] [VARCHAR](511) NOT NULL
    , [ValueType] [VARCHAR](511) NOT NULL
    , [Issuer] [VARCHAR](511) NOT NULL
    , [ExpiryId] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [DF_{TableName}_ExpiryId] DEFAULT NEWSEQUENTIALID()
    , [ExpiryCreatedOn] [DATETIME] NOT NULL
    , [ExpiryModifiedOn] [DATETIME] NOT NULL
    , [ExpiryExpiresOn] [DATETIME] NULL
        CONSTRAINT [DF_{TableName}_ExpiryExpiresOn] DEFAULT NULL
);");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
