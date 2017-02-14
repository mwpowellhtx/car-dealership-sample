namespace Powell.Vehicles.Modules
{
    using Autofac;
    using Autofac.Integration.Mvc;

    public class ControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterControllers(GetType().Assembly);
        }
    }
}
