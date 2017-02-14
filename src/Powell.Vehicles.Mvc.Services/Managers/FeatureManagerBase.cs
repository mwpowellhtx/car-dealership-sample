using System;
using Microsoft.AspNet.Identity;

namespace Powell.Vehicles.Managers
{
    using Identity.Domain;
    using Mvc.Identity;

    public abstract class FeatureManagerBase<TFeature> : RoleManager<TFeature, Guid>
        where TFeature : Feature, new()
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        protected FeatureManagerBase(IIdentityFeatureStore<TFeature> store)
            : base(store)
        {
        }
    }
}
