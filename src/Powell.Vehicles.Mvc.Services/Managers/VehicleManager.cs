namespace Powell.Vehicles.Managers
{
    using Data;

    public class VehicleManager : ControllerManager, IVehicleManager
    {
        public VehicleManager(IHibernateRepository repository)
            : base(repository)
        {
        }
    }
}
