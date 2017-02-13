namespace Powell.Identity
{
    using Powell.Domain;

    public abstract class SecurityExpiringDomainObjectMap<T> : ExpiringDomainObjectMap<T>
        where T : ExpiringDomainObject
    {
        protected override void InitializeSchema()
        {
            base.InitializeSchema();

            Schema("Security");
        }
    }
}
