using Microsoft.Owin.Security.DataProtection;

namespace Powell.Vehicles.Modules
{
    using Autofac;
    using Mvc.Services;

    public class ServicesModule : Module
    {
        private static void RegisterSecurityServices(ContainerBuilder builder)
        {
            builder.RegisterType<BasicDataProtectorTokenProvider>()
                .As<IBasicUserTokenProvider>()
                .InstancePerDependency();

            builder.RegisterType<MachineKeyDataProtector>()
                .As<IMachineKeyDataProtector>()
                .As<IDataProtector>()
                .InstancePerDependency();
        }

        private static void RegisterMessageServices(ContainerBuilder builder)
        {
            builder.RegisterType<EmailService>().As<IEmailIdentityMessageService>().SingleInstance();
            builder.RegisterType<MailKitMessageService>().As<IMailKitMessageService>().SingleInstance();
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterSecurityServices(builder);
            RegisterMessageServices(builder);
        }
    }
}
