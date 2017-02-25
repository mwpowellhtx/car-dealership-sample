namespace Powell.Identity
{
    using Domain;

    public class PermissionMap : SecurityExpiringDomainObjectMap<Permission>
    {
        public PermissionMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            References(x => x.Credential, "CredentialBaseId").Not.Nullable();
            References(x => x.Feature, "FeatureId").Not.Nullable();
            Map(x => x.Privilege).Nullable();
        }
    }
}
