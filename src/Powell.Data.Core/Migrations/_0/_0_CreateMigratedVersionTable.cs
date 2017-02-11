using System;
using System.Data.SqlClient;

namespace Powell.Migrations._0
{
    using MV = MigratedVersion;

    /// <summary>
    /// For academic or demonstration purposes this will be sufficient.
    /// </summary>
    [Migration(0, 0)]
    public class _0_CreateMigratedVersionTable : SqlServerMigrationBase
    {
        /// <summary>
        /// Gets whether ShouldDeleteMigration. Should not delete the migration in this instance
        /// since the migration table will have been dropped.
        /// </summary>
        public override bool ShouldDeleteMigration => false;

        public _0_CreateMigratedVersionTable(SqlConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// "_MigratedVersion"
        /// </summary>
        internal const string TableName = "_MigratedVersion";

        public override void Up()
        {
            var defaultVersion = new Version();

            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NULL
CREATE TABLE [{SchemaName}].[{TableName}]
(
    [{nameof(MV.Id)}] [UNIQUEIDENTIFIER] NOT NULL
        CONSTRAINT [DF{TableName}_{nameof(MV.Id)}] DEFAULT NEWSEQUENTIALID()
        CONSTRAINT [PK{TableName}] PRIMARY KEY
    , [{nameof(MV.VersionText)}] [NVARCHAR](MAX) NOT NULL
        CONSTRAINT [DF{TableName}_{nameof(MV.VersionText)}] DEFAULT N'{defaultVersion}'
    , [{nameof(MV.MigrationTypeFullName)}] [NVARCHAR](MAX) NOT NULL
);");
        }

        public override void Down()
        {
            Sql($@"IF OBJECT_ID(N'[{SchemaName}].[{TableName}]') IS NOT NULL
DROP TABLE [{SchemaName}].[{TableName}];");
        }
    }
}
