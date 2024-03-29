﻿using System.Linq;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Testing.Integration.BaseFixtures;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Integration.ConfigurationTest
{
    [TestFixture]
    public class WindsorContainerInitializerTests : BaseIntegrationFixture
    {
        [Test]
        public void ResolveRepositoryTests()
        {
            var container = Container;

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
