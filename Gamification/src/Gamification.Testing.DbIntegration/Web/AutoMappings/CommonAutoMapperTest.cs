using AutoMapper;
using Gamification.Web.AutoMapper;
using NUnit.Framework;

namespace Gamification.Testing.Integration.Web.AutoMappings
{
    [TestFixture]
    public class CommonAutoMapperTest
    {
        [Test]
        public void AllMappings_ShouldBeValid()
        {
            AutoMappingConfiguration.ConfigureAutoMapper();

            Mapper.AssertConfigurationIsValid();
        }
    }
}
