using System.Data.SqlClient;

namespace Powell.Identity.Domain.Migrations
{
    using Powell.Migrations;

    public abstract class SqlServerSecurityMigrationBase : SqlServerMigrationBase
    {
        /// <summary>
        /// Gets the SchemaName: "Security"
        /// </summary>
        protected override string SchemaName => "Security";

        protected SqlServerSecurityMigrationBase(SqlConnection connection)
            : base(connection)
        {
        }
    }
}
