using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using CommonServiceLocator.WindsorAdapter;
using Gamification.IOC;
using Gamification.Web.AutoMapper;
using Gamification.Web.ControllerFactory;
using Gamification.Web.Controllers;
using Gamification.Web.Utils.ActionFilters;
using Gamification.Web.Utils.CommonViewModels;
using Gamification.Web.Utils.ModelBinders;
using Microsoft.Practices.ServiceLocation;

namespace Gamification.Web
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new EntityNotFoundActionFilterAttribute());
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
                AllTypes.FromAssembly(typeof(HomeController).Assembly).BasedOn<Controller>().LifestyleTransient());
            container.Register(
                Component.For<UrlHelper>().UsingFactoryMethod(x => new UrlHelper(x.Resolve<HttpContextBase>().Request.RequestContext)).LifestylePerWebRequest());

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
            ControllerBuilder.Current.SetControllerFactory(new CastleControllerFactory(container));
            AutoMappingConfiguration.ConfigureAutoMapper();

            RegisterModelBinders();

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterModelBinders()
        {
            ModelBinders.Binders[typeof(DataSource)] = new DataSourceModelBinder();
            ModelBinders.Binders[typeof(FileViewModel)] = new FileViewModelModelBinder();
        }
    }
}