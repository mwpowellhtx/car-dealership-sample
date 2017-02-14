namespace Powell.Vehicles.Modules
{
    using Autofac;
    using Mvc.Year;

    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<YearMapperConfiguration>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
