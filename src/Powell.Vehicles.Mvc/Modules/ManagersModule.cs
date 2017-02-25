using System.Web;
using Microsoft.Owin.Security;

namespace Powell.Vehicles.Modules
{
    using Autofac;
    using Managers;

    public class ManagersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ApplicationUserManager>()
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterType<ApplicationSignInManager>()
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.Register(context => HttpContext.Current.GetOwinContext().Authentication)
                .As<IAuthenticationManager>()
                .InstancePerRequest();

            builder.RegisterType<YearManager>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ManufacturerManager>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ModelManager>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ModelYearManager>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<PaintManager>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<VehicleManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
