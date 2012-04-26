using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Castle.MicroKernel.Registration;
using Gamification.Core.DataAccess;
using Gamification.Core.Specifications;
using Gamification.IOC;
using Gamification.Web.AutoMapper;
using Gamification.Web.ControllerFactory;
using Gamification.Web.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace Gamification.Web
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
#if DEBUG
            filters.Add(new ResetUserAttribute());
#endif
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
                AllTypes.FromAssembly(typeof(HomeController).Assembly).BasedOn<Controller>().LifestyleTransient());
            ControllerBuilder.Current.SetControllerFactory(new CastleControllerFactory(container));
            AutoMappingConfiguration.ConfigureAutoMapper();

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }

    public class ResetUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User != null && filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var repository = ServiceLocator.Current.GetInstance<IUsersRepository>();
                var user = repository.FirstBySpec(new UserByNameSpec(filterContext.HttpContext.User.Identity.Name));
                if (user == null)
                    FormsAuthentication.SignOut();
            }
        }
    }
}