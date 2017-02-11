using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Powell.Vehicles.Mvc.Startup))]
namespace Powell.Vehicles.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
