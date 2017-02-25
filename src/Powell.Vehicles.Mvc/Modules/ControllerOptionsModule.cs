namespace Powell.Vehicles.Modules
{
    using Autofac;
    using Controllers;
    using Mvc.Services;

    public class ControllerOptionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ControllerOptions<ManageController.Result>>()
                .AsImplementedInterfaces()
                .OnActivated(args => args.Instance.Messages = ManageController.GetResultMessages())
                .SingleInstance();
        }
    }
}
