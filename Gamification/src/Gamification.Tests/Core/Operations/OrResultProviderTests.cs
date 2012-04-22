using Gamification.Core.Operations;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Core.Operations
{
    [TestFixture]
    public class OrResultProviderTests : BaseResultProviderTest
    {
        [TestCase(true, true)]
        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void GetResult_ShouldBeResultOfOrOperationBeetwenFirstAndSecondProvider(bool first, bool second)
        {
            var expectedResult = first | second;
            var firstProvider = GetResultProviderWithResult(first);
            var secondProvider = GetResultProviderWithResult(second);
            var orResultProvider = new OrResultProvider(firstProvider, secondProvider);

            var result = orResultProvider.GetResult();

            result.Should().Be.EqualTo(expectedResult);
        }
    }
}