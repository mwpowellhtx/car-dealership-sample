namespace Powell.Domain
{
    using FluentNHibernate.Mapping;

    public abstract class DomainObjectSubclassMap<T> : SubclassMap<T>
    {
        protected DomainObjectSubclassMap()
        {
            Initialize();
        }

        protected virtual void InitializeSchema()
        {
        }

        private void Initialize()
        {
            InitializeSchema();
        }
    }
}
