using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Powell.Migrators
{
    using static SqlDbType;

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

        /// <summary>
        /// Returns the <see cref="SqlDbType"/> corresponding to the <typeparamref name="T"/> type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private SqlDbType GetDbType<T>() => _lazyDbTypes.Value[typeof(T)];

        private readonly Lazy<IDictionary<Type, SqlDbType>> _lazyDbTypes;

        public SqlServerMigrator(string connectionString, params Assembly[] assemblies)
            : base(new SqlConnection(connectionString), assemblies)
        {
            // TODO: TBD: add type mappings as they become evident
            _lazyDbTypes = new Lazy<IDictionary<Type, SqlDbType>>(() =>
                new ConcurrentDictionary<Type, SqlDbType>(
                    new Dictionary<Type, SqlDbType>
                    {
                        {typeof(byte), TinyInt},
                        {typeof(short), SmallInt},
                        {typeof(int), Int},
                        {typeof(long), BigInt},
                        {typeof(double), Float},
                        {typeof(bool), Bit},
                        {typeof(string), NVarChar},
                        {typeof(byte[]), VarBinary},
                        {typeof(Guid), UniqueIdentifier}
                    }));
        }
    }
}
