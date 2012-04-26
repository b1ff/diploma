using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Gamification.IOC;
using Gamification.Web.ControllerFactory;
using Gamification.Web.Controllers;

namespace Gamification.Web
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?" });
            routes.IgnoreRoute("{*urlToCss}", new { urlToCss = @"(.*).css" });
            routes.IgnoreRoute("{*urlToJs}", new { urlToJs = @"(.*).js" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            var container = ComponentRegistrator.BuildWebContainer();
            container.Register(
                AllTypes.FromAssembly(typeof(HomeController).Assembly).BasedOn<Controller>().LifestylePerWebRequest());
            ControllerBuilder.Current.SetControllerFactory(new CastleControllerFactory(container));

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}