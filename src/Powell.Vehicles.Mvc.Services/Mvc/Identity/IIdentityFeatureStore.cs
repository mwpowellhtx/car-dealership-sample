using System;
using Microsoft.AspNet.Identity;

namespace Powell.Vehicles.Mvc.Identity
{
    using Powell.Identity.Domain;

    public interface IIdentityFeatureStore<TFeature> : IRoleStore<TFeature, Guid>
        where TFeature : Feature, new()
    {
    }
}
