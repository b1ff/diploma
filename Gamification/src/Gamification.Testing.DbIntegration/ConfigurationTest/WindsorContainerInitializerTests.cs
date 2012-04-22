using System.Linq;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.IOC;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.DbIntegration.ConfigurationTest
{
    [TestFixture]
    public class WindsorContainerInitializerTests
    {
        [Test]
        public void ResolveRepositoryTests()
        {
            var container = EFComponentRegistrator.GetContainer();

            var repositoryTypes = typeof (BaseEntity).Assembly.GetTypes()
                .Where(x => typeof (BaseEntity).IsAssignableFrom(x) && !x.IsAbstract)
                .Select(x => typeof (IRepository<>).MakeGenericType(x));

            foreach (var repositoryType in repositoryTypes)
            {
                var repository = container.Resolve(repositoryType);
                repository.Should().Not.Be.Null();
            }
        }
    }
}
