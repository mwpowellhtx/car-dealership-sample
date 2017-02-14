namespace Powell.Vehicles.Mvc.Identity
{
    using Data;

    public class IdentityRepository : Repository, IIdentityRepository
    {
        public IdentityRepository(IRepositorySessionProvider provider)
            : base(provider)
        {
        }
    }
}
