namespace Powell.Vehicles.Mvc.Identity
{
    using Powell.Identity.Domain;

    public interface IFeatureRepository<TFeature>
        : IIdentityRepository
            , IIdentityFeatureStore<TFeature>
        where TFeature : Feature, new()
    {
    }

    public interface ICompositeFeatureRepository<TFeature>
        : IFeatureRepository<TFeature>
        where TFeature : Feature, new()
    {
    }

    public interface ICompositeFeatureRepository
        : ICompositeFeatureRepository<Feature>
    {
    }
}
