using Gamification.Core.ResultProviders;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Core.ResultProviders
{
    [TestFixture]
    public class AndResultProviderTests : BaseResultProviderTest
    {
        [TestCase(true, true)]
        [TestCase(false, false)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void GetResult_ShouldReturnResultOfAndBooleanOperation(bool firstOperation, bool secondOperation)
        {
            var expectedResult = firstOperation & secondOperation;
            var mockFirstProvider = GetResultProviderWithResult(firstOperation);
            var mockSecondProvider = GetResultProviderWithResult(secondOperation);
            var andResult = new AndResultProvider(mockFirstProvider, mockSecondProvider);

            var result = andResult.GetResult();

            result.Should().Be.EqualTo(expectedResult);
        }
    }
}
