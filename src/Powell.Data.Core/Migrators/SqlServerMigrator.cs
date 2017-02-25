using System.Data.SqlClient;
using System.Reflection;

namespace Powell.Migrators
{
    using static SqlServerDataBaseUtils;

    public class SqlServerMigrator : MigratorBase<SqlConnection, SqlParameter>
    {
        protected override SqlParameter CreateParameter<T>(string parameterName, T value)
        {
            return new SqlParameter(parameterName, GetDbType<T>()) {Value = value};
        }

        protected override SqlParameter CreateParameter<T>(string parameterName, T value, int size)
        {
            return new SqlParameter(parameterName, GetDbType<T>(), size) {Value = value};
        }

        public SqlServerMigrator(string connectionString, params Assembly[] assemblies)
            : base(new SqlConnection(connectionString), assemblies)
        {
        }
    }
}
