namespace Powell.Vehicles.Managers
{
    using Data;

    public class ModelManager : ControllerManager, IModelManager
    {
        public ModelManager(IHibernateRepository repository)
            : base(repository)
        {
        }
    }
}
