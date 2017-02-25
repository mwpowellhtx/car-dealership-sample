namespace Powell.Identity
{
    using Powell.Domain;

    public abstract class SecurityDomainObjectSubclassMap<T> : DomainObjectSubclassMap<T>
        where T : DomainObject
    {
        protected override void InitializeSchema()
        {
            base.InitializeSchema();

            Schema("Security");
        }
    }
}
