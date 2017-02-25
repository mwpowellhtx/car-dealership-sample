namespace Powell.Vehicles.Modules
{
    using Autofac;
    using Mvc.Identity;

    public class IdentityModule : Module
    {
        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<ICompositeUserRepository>().InstancePerDependency();
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterRepositories(builder);
        }
    }
}
