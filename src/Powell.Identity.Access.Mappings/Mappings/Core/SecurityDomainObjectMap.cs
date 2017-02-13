namespace Powell.Identity
{
    using Powell.Domain;

    public abstract class SecurityDomainObjectMap<T> : DomainObjectMap<T>
        where T : DomainObject
    {
        protected override void InitializeSchema()
        {
            base.InitializeSchema();

            Schema("Security");
        }
    }
}
