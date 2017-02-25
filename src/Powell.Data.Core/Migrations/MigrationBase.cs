using System;
using System.Data.Common;
using System.Reflection;

namespace Powell.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TConnection"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    public abstract class MigrationBase<TConnection, TParameter> : DataBase<TParameter>, IMigration<TParameter>
        where TConnection : DbConnection
        where TParameter : DbParameter
    {
        /// <summary>
        /// Gets whether ShouldDeleteMigration. Default is true.
        /// </summary>
        public virtual bool ShouldDeleteMigration => true;

        /// <summary>
        /// Gets the MigrationTypeFullName.
        /// </summary>
        public string MigrationTypeFullName => GetType().FullName;

        public Version Version => GetType().GetCustomAttribute<MigrationAttribute>()?.Version;

        private readonly TConnection _connection;

        protected MigrationBase(TConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Gets the LastCommandText.
        /// </summary>
        public string LastCommandText { get; private set; }

        /// <summary>
        /// Gets the LastArguments.
        /// </summary>
        public TParameter[] LastArguments { get; private set; }

        /// <summary>
        /// Performs the Sql <paramref name="commandText"/> with optional <paramref name="args"/>.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="args"></param>
        /// <returns>The number of rows affected.</returns>
        protected int Sql(string commandText, params TParameter[] args)
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = LastCommandText = commandText;

                foreach (var arg in LastArguments = args)
                {
                    cmd.Parameters.Add(arg);
                }

                return cmd.ExecuteNonQuery();
            }
        }

        public abstract void Up();

        public abstract void Down();
    }
}
