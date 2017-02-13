namespace Powell.Domain
{
    using FluentNHibernate.Mapping;

    public abstract class DomainObjectMap<T> : ClassMap<T>
        where T : DomainObject
    {
        protected DomainObjectMap()
        {
            Initialize();
        }

        protected virtual void InitializeSchema()
        {
        }

        private void Initialize()
        {
            InitializeSchema();

            Id(x => x.Id).Not.Nullable().GeneratedBy.GuidNative();
        }
    }
}
