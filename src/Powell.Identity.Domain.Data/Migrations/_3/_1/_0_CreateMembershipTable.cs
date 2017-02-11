using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._1
{
    /// <summary>
    /// Creates the Membership Table.
    /// </summary>
    [Migration(3, 1, 0)]
    public class _0_CreateMembershipTable : SqlServerSecurityMigrationBase
    {
        public _0_CreateMembershipTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Membership"
        /// </summary>
        private const string TableName = "Membership";

        public override void Up()
        {
            //TODO: TBD: ditto Credential/Membership re: joining table appropriateness
            //'Unfortunately' we still have to reference [Groups].[CredentialBaseId]
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NULL
CREATE TABLE [{SchemaName}].[{TableName}]
(
    [Id] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [PK_{TableName}] PRIMARY KEY
        CONSTRAINT [DF_{TableName}_Id] DEFAULT NEWSEQUENTIALID()
    , [GroupId] [UNIQUEIDENTIFIER] NULL
        CONSTRAINT [FK_{TableName}_Group] FOREIGN KEY
        REFERENCES [{SchemaName}].[Group] ([CredentialBaseId])
        ON UPDATE NO ACTION ON DELETE NO ACTION
    , [MemberId] [UNIQUEIDENTIFIER] NULL
        CONSTRAINT [FK_{TableName}_CredentialBase] FOREIGN KEY
        REFERENCES [{SchemaName}].[CredentialBase] ([Id])
        ON UPDATE NO ACTION ON DELETE NO ACTION
    , [ExpiryId] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [DF_{SchemaName}_ExpiryId] DEFAULT NEWSEQUENTIALID()
    , [ExpiryCreatedOn] [DATETIME] NOT NULL
    , [ExpiryModifiedOn] [DATETIME] NOT NULL
    , [ExpiryExpiresOn] [DATETIME] NULL
        CONSTRAINT [DF_{SchemaName}_ExiryExpiresOn] DEFAULT NULL
);");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
