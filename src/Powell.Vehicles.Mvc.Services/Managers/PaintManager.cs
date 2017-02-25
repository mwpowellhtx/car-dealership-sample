namespace Powell.Vehicles.Managers
{
    using Data;

    public class PaintManager : ControllerManager, IPaintManager
    {
        public PaintManager(IHibernateRepository repository)
            : base(repository)
        {
        }
    }
}
