namespace Powell.Vehicles.Managers
{
    using Data;

    public class ControllerManager : HibernateManagerBase, IControllerManager
    {
        public ControllerManager(IHibernateRepository repository)
            : base(repository)
        {
        }
    }
}
