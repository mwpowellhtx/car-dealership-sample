namespace Powell.Identity
{
    using Domain;

    public class MembershipMap : SecurityExpiringDomainObjectMap<Membership>
    {
        public MembershipMap()
        {
            Initialize();
        }

        private void Initialize()
        {
            References(x => x.Group, "GroupId").Not.Nullable();
            References(x => x.Member, "MemberId").Not.Nullable();
        }
    }
}
