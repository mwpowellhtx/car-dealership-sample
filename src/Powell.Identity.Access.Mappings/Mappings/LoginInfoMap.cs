namespace Powell.Identity
{
    using Domain;

    public class LoginInfoMap : SecurityDomainObjectMap<LoginInfo>
    {
        public LoginInfoMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            References(x => x.User, "UserId").Not.Nullable();
            Map(x => x.Provider).CustomSqlType("VARCHAR").Length(127).Not.Nullable();
            Map(x => x.ProviderKey).CustomSqlType("VARCHAR(MAX)").Not.Nullable();
        }
    }
}
