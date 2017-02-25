namespace Powell.Identity
{
    using Domain;

    public class GroupSubclassMap : SecurityDomainObjectSubclassMap<Group>
    {
        public GroupSubclassMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            KeyColumn("CredentialBaseId");

            Map(x => x.Description).CustomSqlType("NVARCHAR(MAX)").Not.Nullable();
        }
    }
}
