using Gamification.Core.Operations;
using Moq;

namespace Gamification.Testing.Unit.Core.Operations
{
    public abstract class BaseResultProviderTest
    {
        protected IBooleanResultProvider GetResultProviderWithResult(bool neededResult)
        {
            var mock = new Mock<IBooleanResultProvider>();
            mock.Setup(x => x.GetResult()).Returns(neededResult);
            return mock.Object;
        }
    }
}