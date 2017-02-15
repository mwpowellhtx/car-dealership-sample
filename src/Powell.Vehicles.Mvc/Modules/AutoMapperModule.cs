namespace Powell.Vehicles.Modules
{
    using Autofac;
    using Mvc.Manufacturer;
    using Mvc.Model;
    using Mvc.ModelYear;
    using Mvc.Paint;
    using Mvc.Year;

    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<YearMapperConfiguration>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ManufacturerMapperConfiguration>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ModelMapperConfiguration>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ModelYearMapperConfiguration>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<PaintMapperConfiguration>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
