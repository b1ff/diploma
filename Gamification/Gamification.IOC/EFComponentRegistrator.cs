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
        public static IWindsorContainer GetContainer()
        {
            var container = new WindsorContainer();

            container.AddFacility<WcfFacility>();
            container.Register(
                Component.For<IApplicationSettings>().ImplementedBy<FromConfigFileConfiguration>().LifeStyle.Singleton);
            container.Register(Component.For<EfDbContext>().UsingFactoryMethod(x =>
                                                                                   {
                                                                                       var appSettings = x.Resolve<IApplicationSettings>();
                                                                                       return new EfDbContext(appSettings.ConnectionString);
                                                                                   }).LifeStyle.PerThread);
            container.Register(
                    AllTypes.FromAssembly(typeof(EfRepository<>).Assembly)
                        .Pick()
                        .WithService.DefaultInterfaces()
                        .LifestylePerThread());

            return container;
        }
    }
}
