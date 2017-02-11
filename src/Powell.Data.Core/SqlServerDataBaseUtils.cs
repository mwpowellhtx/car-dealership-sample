using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;

namespace Powell
{
    using static SqlDbType;

    internal static class SqlServerDataBaseUtils
    {
        /// <summary>
        /// Returns the <see cref="SqlDbType"/> corresponding to the <typeparamref name="T"/> type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static SqlDbType GetDbType<T>() => LazyDbTypes.Value[typeof(T)];

        private static Lazy<IDictionary<Type, SqlDbType>> LazyDbTypes { get; }

        static SqlServerDataBaseUtils()
        {
            // TODO: TBD: add type mappings as they become evident
            LazyDbTypes = new Lazy<IDictionary<Type, SqlDbType>>(() =>
                new ConcurrentDictionary<Type, SqlDbType>(
                    new Dictionary<Type, SqlDbType>
                    {
                        {
                            typeof(byte), TinyInt
                        },
                        {
                            typeof(short), SmallInt
                        },
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
