using System;

namespace Powell.Migrations
{
    /// <summary>
    /// Represents a Migration concern.
    /// </summary>
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
        /// Gets the LastArguments.
        /// </summary>
        object[] LastArguments { get; }

        /// <summary>
        /// Gets whether ShouldDeleteMigration. Default is true, which is satisfactory for every
        /// circumstance but for the base migration.
        /// </summary>
        bool ShouldDeleteMigration { get; }
    }
}
