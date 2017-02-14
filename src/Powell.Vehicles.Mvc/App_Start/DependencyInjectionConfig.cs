using System;
using System.Reflection;
using System.Web.Mvc;

namespace Powell.Vehicles
{
    using Autofac;
    using Autofac.Integration.Mvc;

    public static class DependencyInjectionConfig
    {
        private static readonly Lazy<IContainer> PrivateContainer;

        internal static IContainer Container => PrivateContainer.Value;

        static DependencyInjectionConfig()
        {
            PrivateContainer = new Lazy<IContainer>(() =>
            {
                var builder = new ContainerBuilder();

                // This does the heavy lifting of the type registrations.
                builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

                var container = builder.Build();

                // This is required to overcome the default DR, which apparently forces param-less ctors.
                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

                //// TODO: TBD: carry over from another project; may or may not be helpful here, TBD...
                //// Setup global sitemap loader (required).
                //SiteMaps.Loader = container.Resolve<ISiteMapLoader>();

                return container;
            });
        }
    }
}
