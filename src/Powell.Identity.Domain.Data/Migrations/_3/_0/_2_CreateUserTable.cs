using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._0
{
    /// <summary>
    /// Creates the Security User Table.
    /// </summary>
    [Migration(3, 0, 2)]
    public class _2_CreateUserTable : SqlServerSecurityMigrationBase
    {
        public _2_CreateUserTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "User"
        /// </summary>
        private const string TableName = "User";

        public override void Up()
        {
            // TODO: TBD: a joining Credential/Membership table might be more appropriate...
            // TODO: TBD: how wide to make the VARBINARY column? MAX seems like overkill, but something larger than 64? 128? 256? 1024?
            // TODO: TBD: for now giving PasswordHash 511 characters, at most; this is probably overkill.
            // Hashed password database column requirements RSS: http://forums.asp.net/p/2086489/6025968.aspx?p=True&t=635921740352044943

            // The Check Constraint for email address is pretty weak at present, but this is the minimum in my estimation.
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NULL
CREATE TABLE [{SchemaName}].[{TableName}]
(
    [CredentialBaseId] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [PK_{TableName}] PRIMARY KEY
        CONSTRAINT [FK_{TableName}_CredentialBase] FOREIGN KEY
        REFERENCES [{SchemaName}].[CredentialBase] ([Id])
        ON UPDATE NO ACTION ON DELETE NO ACTION
    , [Id] AS [CredentialBaseId]
    , [EmailAddress] [VARCHAR](319) NOT NULL
    , [EmailAddressConfirmed] [BIT] NOT NULL
        CONSTRAINT [DF_{TableName}_EmailAddressConfirmed] DEFAULT 0
    , [LockedOut] [BIT] NOT NULL
        CONSTRAINT [DF_{TableName}_LockedOut] DEFAULT 0
    , [LockoutExpiryUtc] [DATETIME] NULL
        CONSTRAINT [DF_{TableName}_LockoutExpiryUtc] DEFAULT NULL
    , [PasswordHash] [VARCHAR](511) NOT NULL
    , [AccessFailedCount] [INT] NOT NULL
        CONSTRAINT [DF_{TableName}_AccessFailedCount] DEFAULT 0
    , [SecurityStamp] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [DF_{TableName}_SecurityStamp] DEFAULT NEWID()
    , [TwoFactorEnabled] [BIT] NOT NULL
        CONSTRAINT [DF_{TableName}_TwoFactorEnabled] DEFAULT 0
    , CONSTRAINT [CK_{TableName}_EmailAddress] CHECK (LEN([EmailAddress]) > 3)
    , CONSTRAINT [UQ_{TableName}_EmailAddress] UNIQUE ([EmailAddress])
)");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
