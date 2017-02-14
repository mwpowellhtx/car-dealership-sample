namespace Powell.Vehicles.Managers
{
    using Data;

    public abstract class HibernateManagerBase : ManagerBase<IHibernateRepository>, IHibernateManagerBase
    {
        protected HibernateManagerBase(IHibernateRepository repository)
            : base(repository)
        {
        }
    }
}
