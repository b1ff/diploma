using System.Web;
using Castle.Core;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Gamification.Core.ProjectSettings;
using Gamification.Data.EF;
using Gamification.Data.EF.Contexts;

namespace Gamification.IOC
{
    public class EFComponentRegistrator 
    {
        public static IWindsorContainer BaseGetContainer()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IApplicationSettings>().ImplementedBy<FromConfigFileConfiguration>().LifeStyle.Singleton);
            container.Register(Component.For<IWindsorContainer>().Instance(container));

            return container;
        }

        public static IWindsorContainer GetWebContainer()
        {
            var container = BaseGetContainer();
            container.Register(Component.For<EfDbContext>().UsingFactoryMethod(x =>
            {
                var appSettings = x.Resolve<IApplicationSettings>();
                return new EfDbContext(appSettings.ConnectionString);
            }).LifeStyle.PerWebRequest);

            container.Register(AllTypes.FromAssembly(typeof(EfRepository<>).Assembly).Pick().WithService.DefaultInterfaces().LifestylePerWebRequest());
            container.Register(Component.For<HttpContextBase>().UsingFactoryMethod(x => new HttpContextWrapper(HttpContext.Current)).LifeStyle.PerWebRequest);

            return container;
        }

        public static IWindsorContainer GetWcfContainer()
        {
            var container = BaseGetContainer();
            container.AddFacility<WcfFacility>();
            container.Register(Component.For<EfDbContext>().UsingFactoryMethod(x =>
            {
                var appSettings = x.Resolve<IApplicationSettings>();
                return new EfDbContext(appSettings.ConnectionString);
            }).LifeStyle.PerWcfOperation());

            container.Register(AllTypes.FromAssembly(typeof(EfRepository<>).Assembly).Pick().WithService.DefaultInterfaces().LifestylePerWcfOperation());
            container.Register(Component.For<HttpContextBase>().UsingFactoryMethod(x => new HttpContextWrapper(HttpContext.Current)).LifeStyle.PerWcfOperation());

            return container;
        }
    }
}