using System;
using System.Web;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using CommonServiceLocator.WindsorAdapter;
using Gamification.Core.ProjectSettings;
using Gamification.IOC;
using Gamification.WebServices.ServicesContracts;
using Microsoft.Practices.ServiceLocation;

namespace Gamification.WebServices
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var container = ComponentRegistrator.BuildWcfContainer();
            container.Register(
                AllTypes.FromAssembly(typeof(IActionsService).Assembly).Pick().WithService.DefaultInterfaces().
                    LifestylePerWcfOperation());

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));

            AutoConfigurator.PerformAllConfiguration(typeof(ActionsService).Assembly);
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            EnableCrossDomainAjaxCall();
        }

        private void EnableCrossDomainAjaxCall()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }
    }
}