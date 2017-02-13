namespace Powell.Identity
{
    using Domain;

    public class FeatureMap : SecurityExpiringDomainObjectMap<Feature>
    {
        public FeatureMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            References(x => x.Parent, "ParentId").Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR").Length(128).Not.Nullable();
            Map(x => x.Description).CustomSqlType("NVARCHAR(MAX)").Not.Nullable();
            HasMany(x => x.Children);
            HasMany(x => x.Permissions);
        }
    }
}
