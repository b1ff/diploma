using System;
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
            container.Register(AllTypes.FromAssembly(typeof(IActionsService).Assembly).Pick().WithService.DefaultInterfaces().LifestylePerWcfOperation());

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));

            AutoConfigurator.PerformAllConfiguration(typeof(ActionsService).Assembly);
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}