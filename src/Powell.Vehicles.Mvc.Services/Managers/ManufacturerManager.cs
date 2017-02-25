namespace Powell.Vehicles.Managers
{
    using Data;

    public class ManufacturerManager : ControllerManager, IManufacturerManager
    {
        public ManufacturerManager(IHibernateRepository repository)
            : base(repository)
        {
        }
    }
}
