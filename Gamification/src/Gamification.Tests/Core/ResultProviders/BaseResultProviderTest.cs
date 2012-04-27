using Gamification.Core.ResultProviders;
using Moq;

namespace Gamification.Testing.Unit.Core.ResultProviders
{
    public abstract class BaseResultProviderTest
    {
        protected BooleanResultProvider GetResultProviderWithResult(bool neededResult)
        {
            var mock = new Mock<BooleanResultProvider>();
            mock.Setup(x => x.GetResult()).Returns(neededResult);
            return mock.Object;
        }
    }
}