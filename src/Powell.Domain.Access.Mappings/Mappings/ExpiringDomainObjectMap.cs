namespace Powell.Domain
{
    public abstract class ExpiringDomainObjectMap<T> : DomainObjectMap<T>
        where T : ExpiringDomainObject
    {
        protected ExpiringDomainObjectMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            Component(x => x.Expiry).ColumnPrefix("Expiry");
        }
    }
}
