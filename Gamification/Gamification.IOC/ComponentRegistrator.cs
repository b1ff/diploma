using System.Web;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Gamification.Core.ProjectSettings;
using Gamification.Data.EF;
using Gamification.Data.EF.Contexts;
using Gamification.Data.EF.Repositories;
using Gamification.Web.Utils.SimpleMembership;
using Microsoft.Practices.ServiceLocation;

namespace Gamification.IOC
{
    public class ComponentRegistrator 
    {
        public static IWindsorContainer BuildContainerWithCommonComponents()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IApplicationSettings>().ImplementedBy<FromConfigFileConfiguration>().LifeStyle.Singleton);
            container.Register(Component.For<IWindsorContainer>().Instance(container).LifestyleSingleton());

            container.Register(Component.For<EfDbContext>().UsingFactoryMethod(x =>
            {
                var appSettings = x.Resolve<IApplicationSettings>();
                return new EfDbContext(appSettings.ConnectionString);
            }).LifeStyle.Transient);

            container.Register(AllTypes.FromAssembly(typeof(EfRepository<>).Assembly).Pick().WithService.DefaultInterfaces().LifestyleTransient());

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
            return container;
        }

        public static IWindsorContainer BuildWebContainer()
        {
            var container = BuildContainerWithCommonComponents();
            container.Register(AllTypes.FromAssembly(typeof(ISimpleMembership).Assembly).Pick().WithService.DefaultInterfaces().LifestyleTransient());
            container.Register(Component.For<HttpContextBase>().UsingFactoryMethod(x => new HttpContextWrapper(HttpContext.Current)).LifeStyle.PerWebRequest);

            return container;
        }

        public static IWindsorContainer BuildWcfContainer()
        {
            var container = BuildContainerWithCommonComponents();
            container.AddFacility<WcfFacility>();

            return container;
        }
    }
}