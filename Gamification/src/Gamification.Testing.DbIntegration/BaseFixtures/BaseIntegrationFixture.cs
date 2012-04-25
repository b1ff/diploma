using Castle.Windsor;
using Gamification.Testing.DbIntegration.Utils;
using NUnit.Framework;

namespace Gamification.Testing.DbIntegration.BaseFixtures
{
    [TestFixture]
    public abstract class BaseIntegrationFixture
    {
        public IWindsorContainer Container { get; private set; }

        [SetUp]
        public void SetUp()
        {
            Container = IntegrationTestsComponentRegistration.CreateTestsWindsorContainer();
            OnSetup();
        }

        protected virtual void OnSetup(){}
        protected virtual void OnTeardown(){}
        
        [TearDown]
        public void TearDown()
        {
            OnTeardown();
            Container.Dispose();
        }
    }
}
