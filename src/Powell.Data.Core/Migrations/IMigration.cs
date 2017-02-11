using System;

namespace Powell.Migrations
{
    public interface IMigration
    {
        /// <summary>
        /// Gets the <see cref="System.Version"/>corresponding with the <see cref="IMigration"/>.
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// Gets the MigrationTypeFullName.
        /// </summary>
        string MigrationTypeFullName { get; }

        void Up();

        void Down();

        /// <summary>
        /// Gets the LastCommandText.
        /// </summary>
        string LastCommandText { get; }

        /// <summary>
        /// Gets whether ShouldDeleteMigration. Default is true, which is satisfactory for every
        /// circumstance but for the base migration.
        /// </summary>
        bool ShouldDeleteMigration { get; }
    }

    /// <summary>
    /// Represents a Migration concern.
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public interface IMigration<out TParameter> : IMigration
    {
        /// <summary>
        /// Gets the LastArguments.
        /// </summary>
        TParameter[] LastArguments { get; }
    }
}
