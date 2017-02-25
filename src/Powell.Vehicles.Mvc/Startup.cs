using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Powell.Vehicles.Startup))]
namespace Powell.Vehicles
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
