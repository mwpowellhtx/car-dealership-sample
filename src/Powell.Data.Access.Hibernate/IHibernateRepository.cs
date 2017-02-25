namespace Powell.Data
{
    using NHibernate;

    public interface IHibernateRepository : IRepository<ISession>
    {
    }
}
