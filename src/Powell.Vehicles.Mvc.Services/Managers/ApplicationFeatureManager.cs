namespace Powell.Vehicles.Managers
{
    using Identity.Domain;
    using Mvc.Identity;

    public class ApplicationFeatureManager : FeatureManagerBase<Feature>
    {
        public ApplicationFeatureManager(ICompositeFeatureRepository repository)
            : base(repository)
        {
        }
    }
}
