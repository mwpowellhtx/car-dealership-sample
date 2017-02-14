namespace Powell.Vehicles.Mvc.Identity
{
    using Data;
    using NHibernate;

    public interface IIdentityRepository : IRepository<ISession>
    {
    }
}
