namespace Powell.Identity
{
    using Domain;

    public class UserSubclassMap : SecurityDomainObjectSubclassMap<User>
    {
        public UserSubclassMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            KeyColumn(@"CredentialBaseId");
            Map(x => x.EmailAddress).CustomSqlType("VARCHAR").Length(64 + 254 + 1).Not.Nullable();
            Map(x => x.IsLockedOut, "LockedOut").Not.Nullable();
            Map(x => x.LockoutExpiryUtc).Not.Nullable();
            Map(x => x.PasswordHash).CustomSqlType("VARCHAR").Length(511).Not.Nullable();
            Map(x => x.AccessFailedCount).Not.Nullable();
            Map(x => x.SecurityStamp).Not.Nullable();
            Map(x => x.IsTwoFactorEnabled, "TwoFactorEnabled").Not.Nullable();
            HasMany(x => x.Claims);
        }
    }
}
