namespace Powell.Identity
{
    using Domain;

    public class CredentialBaseMap : SecurityExpiringDomainObjectMap<CredentialBase>
    {
        public CredentialBaseMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            Map(x => x.Name).CustomSqlType("NVARCHAR").Length(128).Not.Nullable();
            HasMany(x => x.MemberOf).Where(x => x.Member.Id == x.Id);
            HasMany(x => x.Permissions).Where(x => x.Credential.Id == x.Id);
        }
    }
}
