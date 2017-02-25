using System.Data.SqlClient;

namespace Powell.Migrations
{
    using static SqlServerDataBaseUtils;

    /// <summary>
    /// For academic or demonstration purposes this will be sufficient.
    /// </summary>
    public abstract class SqlServerMigrationBase : MigrationBase<SqlConnection, SqlParameter>
    {
        protected override SqlParameter CreateParameter<T>(string parameterName, T value)
        {
            return new SqlParameter(parameterName, GetDbType<T>()) { Value = value };
        }

        protected override SqlParameter CreateParameter<T>(string parameterName, T value, int size)
        {
            return new SqlParameter(parameterName, GetDbType<T>(), size) { Value = value };
        }

        /// <summary>
        /// SchemaName defaults to "dbo".
        /// </summary>
        protected virtual string SchemaName { get; }

        /// <summary>
        /// Protected constructor. The <paramref name="connection"/> is not managed by the
        /// migration. In other words, will not be disposed by the migration itself.
        /// </summary>
        /// <param name="connection"></param>
        protected SqlServerMigrationBase(SqlConnection connection)
            : base(connection)
        {
            SchemaName = "dbo";
        }
    }
}
