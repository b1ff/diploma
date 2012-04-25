using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Gamification.Core.ProjectSettings;
using Gamification.Data.EF;
using Gamification.Data.EF.Contexts;
using Gamification.IOC;

namespace Gamification.Testing.DbIntegration.Utils
{
    public static class IntegrationTestsComponentRegistration
    {
        public static IWindsorContainer CreateTestsWindsorContainer()
        {
            var container = EFComponentRegistrator.BaseGetContainer();
            container.Register(Component.For<EfDbContext>().UsingFactoryMethod(x =>
            {
                var appSettings = x.Resolve<IApplicationSettings>();
                return new EfDbContext(appSettings.ConnectionString);
            }).LifeStyle.Transient);

            container.Register(AllTypes.FromAssembly(typeof(EfRepository<>).Assembly).Pick().WithService.DefaultInterfaces().LifestyleTransient());
            return container;
        }
    }
}
