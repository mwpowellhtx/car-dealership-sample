using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._0
{
    /// <summary>
    /// Creates the Security LoginInfo Table.
    /// </summary>
    [Migration(3, 0, 4)]
    public class _4_CreateLoginInfoTable : SqlServerSecurityMigrationBase
    {
        public _4_CreateLoginInfoTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "LoginInfo"
        /// </summary>
        private const string TableName = "LoginInfo";

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
    , [Provider] [VARCHAR](127) NOT NULL
    , [ProviderKey] [VARCHAR](MAX) NOT NULL
);");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
