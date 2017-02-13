using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations._3._1
{
    /// <summary>
    /// Creates the Features Table.
    /// </summary>
    [Migration(3, 1, 1)]
    public class _1_CreateFeatureTable : SqlServerSecurityMigrationBase
    {
        public _1_CreateFeatureTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "Feature"
        /// </summary>
        private const string TableName = "Feature";

        public override void Up()
        {
            //TODO: TBD: ditto Credential/Membership re: joining table appropriateness
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NULL
CREATE TABLE [{SchemaName}].[{TableName}]
(
    [Id] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [PK_{TableName}] PRIMARY KEY
        CONSTRAINT [DF_{TableName}_Id] DEFAULT NEWSEQUENTIALID()
    , [ParentId] [UNIQUEIDENTIFIER] NULL
        CONSTRAINT [FK_{TableName}_Parent] FOREIGN KEY
        REFERENCES [{SchemaName}].[{TableName}] ([Id])
        ON UPDATE NO ACTION ON DELETE NO ACTION
    , [Name] [NVARCHAR](128) NOT NULL
    , [Description] [NVARCHAR](MAX) NOT NULL
        CONSTRAINT [DF_{TableName}_Description] DEFAULT N''
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
