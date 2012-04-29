using System.Web;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Gamification.Core.ProjectSettings;
using Gamification.Data.EF.Contexts;
using Gamification.Data.EF.Repositories;
using Gamification.IOC;
using Gamification.Web.Utils.SimpleMembership;
using Moq;

namespace Gamification.Testing.Integration.Utils
{
    public static class IntegrationTestsComponentRegistration
    {
        public static IWindsorContainer CreateTestsWindsorContainer()
        {
            var container = ComponentRegistrator.BuildContainerWithCommonComponents();
            container.Register(Component.For<EfDbContext>().UsingFactoryMethod(x =>
            {
                var appSettings = x.Resolve<IApplicationSettings>();
                return new EfDbContext(appSettings.ConnectionString);
            }).LifeStyle.PerThread);

            container.Register(
                Component.For<HttpContextBase>().UsingFactoryMethod(x => new Mock<HttpContextBase>().Object).LifeStyle.Transient);
            container.Register(AllTypes.FromAssembly(typeof(EfRepository<>).Assembly).Pick().WithService.DefaultInterfaces().LifestyleTransient());
            container.Register(AllTypes.FromAssembly(typeof(ISimpleMembership).Assembly).Pick().WithService.DefaultInterfaces().LifestyleTransient());
            return container;
        }
    }
}
