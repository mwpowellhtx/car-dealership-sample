namespace Powell.Identity
{
    using Domain;

    public class ClaimMap : SecurityExpiringDomainObjectMap<Claim>
    {
        public ClaimMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            References(x => x.User, "UserId").Not.Nullable();
            Map(x => x.TypeUri).CustomSqlType("VARCHAR").Length(511).Not.Nullable();
            Map(x => x.Value).CustomSqlType("VARCHAR").Length(511).Not.Nullable();
            Map(x => x.ValueType).CustomSqlType("VARCHAR").Length(511).Not.Nullable();
            Map(x => x.Issuer).CustomSqlType("VARCHAR").Length(511).Not.Nullable();
        }
    }
}
