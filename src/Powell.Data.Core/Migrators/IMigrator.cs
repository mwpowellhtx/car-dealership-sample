using System;

namespace Powell.Migrators
{
    using Migrations;

    public interface IMigrator : IDisposable
    {
        /// <summary>
        /// Applies all of the <see cref="IMigration"/>.
        /// </summary>
        /// <returns></returns>
        IMigrator Up();

        /// <summary>
        /// Applies the gathered <see cref="IMigration"/> Up <paramref name="toVersion"/>.
        /// </summary>
        /// <param name="toVersion"></param>
        /// <returns></returns>
        IMigrator Up(Version toVersion);

        /// <summary>
        /// Unwinds all of the <see cref="IMigration"/>.
        /// </summary>
        /// <returns></returns>
        IMigrator Down();

        /// <summary>
        /// Applies the gathered <see cref="IMigration"/> Down <paramref name="toVersion"/>.
        /// </summary>
        /// <param name="toVersion"></param>
        /// <returns></returns>
        IMigrator Down(Version toVersion);
    }
}
