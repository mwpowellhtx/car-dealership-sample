using System;

namespace Powell
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class MigrationAttribute : Attribute
    {
        internal Version Version { get; }

        private const int Default = default(int);

        public MigrationAttribute(int major, int minor)
            : this(major, minor, Default, Default)
        {
        }

        public MigrationAttribute(int major, int minor, int build)
            : this(major, minor, build, Default)
        {
        }

        public MigrationAttribute(int major, int minor, int build, int revision)
        {
            Version = new Version(major, minor, build, revision);
        }
    }
}
