using System.Web.Mvc;
using System.Web.Routing;

namespace Powell.Vehicles
{
    using static UrlParameter;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //// TODO: TBD: ? doesn't seem to work properly? i.e. ~/Model/Get/Available/Years/... no joy...
            // routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = Optional}
            );
        }
    }
}
