using System;

namespace Powell
{
    public class MigratedVersion : DomainObject
    {
        private Version _version;

        public Version Version
        {
            get { return _version; }
            private set { _version = value ?? new Version(); }
        }

        public string VersionText
        {
            get { return Version.ToString(); }
            set { Version = Version.Parse(value ?? new Version().ToString()); }
        }

        public Type MigrationType { get; private set; }

        public string MigrationTypeFullName
        {
            get { return MigrationType?.FullName ?? string.Empty; }
            set { MigrationType = Type.GetType(value); }
        }

        internal MigratedVersion()
        {
            Initialize();
        }

        private void Initialize()
        {
            Version = null;
        }
    }
}
