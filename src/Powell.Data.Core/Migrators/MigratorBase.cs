using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace Powell.Migrators
{
    using Collections.Generic;
    using Migrations;
    using Migrations._0;
    using MV = MigratedVersion;
    using static ConnectionState;

    /// <summary>
    /// For academic or demonstration purposes this will be sufficient.
    /// </summary>
    /// <typeparam name="TConnection"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    public abstract class MigratorBase<TConnection, TParameter> : DataBase<TParameter>, IMigrator
        where TConnection : DbConnection
        where TParameter : DbParameter
    {
        private readonly TConnection _connection;

        /// <summary>
        /// Returns a newly created <see cref="IMigration"/>. We will pass the
        /// <see cref="_connection"/> along to each migration. However, that does not pass
        /// ownership of the connection.
        /// </summary>
        /// <param name="migrationType"></param>
        /// <returns></returns>
        private IMigration CreateMigration(Type migrationType)
        {
            // Not expecting the ctor to be null.
            var ctor = migrationType.GetConstructor(new[] {typeof(TConnection)});
            // ReSharper disable once PossibleNullReferenceException
            return (IMigration) ctor.Invoke(new object[] {_connection});
        }

        private void InsertMigration(IMigration migration)
        {
            const string schemaName = "dbo";
            const string tableName = _0_CreateMigratedVersionTable.TableName;

            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText
                    = $"INSERT INTO [{schemaName}].[{tableName}]"
                      + $" ([{nameof(MV.VersionText)}], [{nameof(MV.MigrationTypeFullName)}])"
                      + $" VALUES (@0, @1)";

                cmd.Parameters.Add(CreateParameter("0", migration.Version.ToString()));
                cmd.Parameters.Add(CreateParameter("1", migration.MigrationTypeFullName));

                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteMigration(IMigration migration)
        {
            const string schemaName = "dbo";
            const string tableName = _0_CreateMigratedVersionTable.TableName;

            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText
                    = $"DELETE FROM [{schemaName}].[{tableName}]"
                      + $" WHERE [{nameof(MV.VersionText)}]=@0"
                      + $" AND [{nameof(MV.MigrationTypeFullName)}]=@1";

                cmd.Parameters.Add(CreateParameter("0", migration.Version.ToString()));
                cmd.Parameters.Add(CreateParameter("1", migration.MigrationTypeFullName));

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Returns whether the <paramref name="migration"/> HasApplied.
        /// </summary>
        /// <param name="migration"></param>
        /// <returns></returns>
        private bool HasApplied(IMigration migration)
        {
            const string schemaName = "dbo";
            const string tableName = _0_CreateMigratedVersionTable.TableName;

            /* TODO: TBD: managing database migrations is a perennial issue for which there is a plethora
             * of tooling that helps get the job done, none of which is perfect, and all of which requires
             * extreme level of discipline and attention to detail; this particular approach assumes that
             * the presentation of types/versions is disciplined. */

            var m = migration;

            // Rule out the scenario where the Version table does not exist yet.
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT OBJECT_ID(N'[{schemaName}].[{tableName}]');";
                var id = cmd.ExecuteScalar();
                if (id is DBNull) return false;
            }

            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText
                    = $"SELECT COUNT(*) FROM [{schemaName}].[{tableName}]"
                      + $" WHERE [{nameof(MV.VersionText)}]=@0"
                      + $" AND [{nameof(MV.MigrationTypeFullName)}]=@1";

                cmd.Parameters.Add(CreateParameter("0", m.Version.ToString()));
                cmd.Parameters.Add(CreateParameter("1", m.MigrationTypeFullName));

                var count = (int) cmd.ExecuteScalar();

                return count == 1;
            }
        }

        /// <summary>
        /// Returns whether the <paramref name="migration"/> HasNotApplied.
        /// </summary>
        /// <param name="migration"></param>
        /// <returns></returns>
        private bool HasNotApplied(IMigration migration) => !HasApplied(migration);

        /// <summary>
        /// Returns the gathered <see cref="IMigration"/> across the <paramref name="assemblies"/>
        /// in ascending <see cref="Version"/> order. Does not know whether we want the ones that
        /// have not been applied.
        /// </summary>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        private IEnumerable<IMigration> GatherMigrations(params Assembly[] assemblies)
        {
            // TODO: TBD: also check whether the migration has been previously applied...
            var interfaceType = typeof(IMigration);

            // Make sure that all of the migrations are gathered and evaluated prior to next steps.
            return assemblies.SelectMany(
                    assy => assy.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract)
                        .Where(t => interfaceType.IsAssignableFrom(t))
                        .Select(t => new {Type = t, Attr = t.GetCustomAttribute<MigrationAttribute>()}))
                .Where(x => x.Attr != null)
                .OrderBy(x => x.Attr.Version)
                .Select(x => CreateMigration(x.Type)).ToArray();
        }

        private readonly Lazy<IEnumerable<IMigration>> _lazyMigrations;

        private IEnumerable<IMigration> Migrations => _lazyMigrations.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="assemblies"></param>
        protected MigratorBase(TConnection connection, params Assembly[] assemblies)
        {
            _connection = connection;

            // Migrator runs with Open Connection.
            if (connection.State != Open)
            {
                connection.OpenAsync().Wait();
            }

            // Gather the Migrations at the moment we need them, but not before.
            _lazyMigrations = new Lazy<IEnumerable<IMigration>>(() => GatherMigrations(assemblies));
        }

        /// <summary>
        /// Performs the <see cref="IMigration.Up"/> action on the <paramref name="migration"/>.
        /// </summary>
        /// <param name="migration"></param>
        private void Upgrade(IMigration migration)
        {
            try
            {
                migration.Up();
                InsertMigration(migration);
            }
            catch (Exception)
            {
                // TODO: log and/or roll up the actual sql that was used to perform the upgrade
                throw;
            }
        }

        /// <summary>
        /// Performs the <see cref="IMigration.Down"/> action on the <paramref name="migration"/>.
        /// </summary>
        /// <param name="migration"></param>
        private void Downgrade(IMigration migration)
        {
            try
            {
                migration.Down();

                if (migration.ShouldDeleteMigration)
                {
                    DeleteMigration(migration);
                }
            }
            catch (Exception)
            {
                // TODO: log and/or roll up the actual sql that was used to perform the downgrade
                throw;
            }
        }

        /// <summary>
        /// Upgrades the <see cref="Migrations"/> up <paramref name="toVersion"/> that each <see cref="HasNotApplied"/>.
        /// </summary>
        /// <param name="toVersion"></param>
        /// <returns></returns>
        public IMigrator Up(Version toVersion)
        {
            Migrations.Where(m => m.Version <= toVersion).Where(HasNotApplied).ForEach(Upgrade);
            return this;
        }

        /// <summary>
        /// Upgrades all of the <see cref="Migrations"/> that each <see cref="HasNotApplied"/>.
        /// </summary>
        /// <returns></returns>
        public IMigrator Up()
        {
            Migrations.Where(HasNotApplied).ForEach(Upgrade);
            return this;
        }

        /// <summary>
        /// Downgrades the <see cref="Migrations"/> down <paramref name="toVersion"/> that each <see cref="HasApplied"/>.
        /// </summary>
        /// <param name="toVersion"></param>
        /// <returns></returns>
        public IMigrator Down(Version toVersion)
        {
            Migrations.Where(m => m.Version >= toVersion).Where(HasApplied).OrderByDescending(m => m.Version).ForEach(Downgrade);
            return this;
        }

        /// <summary>
        /// Downgrades all of the <see cref="Migrations"/> that each <see cref="HasApplied"/>.
        /// </summary>
        /// <returns></returns>
        public IMigrator Down()
        {
            Migrations.Where(HasApplied).OrderByDescending(m => m.Version).ForEach(Downgrade);
            return this;
        }

        protected bool IsDisposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                _connection.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            IsDisposed = true;
        }
    }
}
