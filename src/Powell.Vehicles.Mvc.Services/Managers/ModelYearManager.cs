namespace Powell.Vehicles.Managers
{
    using Data;

    public class ModelYearManager : ControllerManager, IModelYearManager
    {
        public ModelYearManager(IHibernateRepository repository)
            : base(repository)
        {
        }
    }
}
