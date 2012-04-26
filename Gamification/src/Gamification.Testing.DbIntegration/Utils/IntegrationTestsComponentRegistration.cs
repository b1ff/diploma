using Castle.Windsor;
using Gamification.IOC;

namespace Gamification.Testing.Integration.Utils
{
    public static class IntegrationTestsComponentRegistration
    {
        public static IWindsorContainer CreateTestsWindsorContainer()
        {
            var container = ComponentRegistrator.BuildContainerWithCommonComponents();
            return container;
        }
    }
}
