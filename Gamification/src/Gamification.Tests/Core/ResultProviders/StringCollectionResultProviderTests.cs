using Gamification.Core.Entities;
using Gamification.Core.Entities.Constraints;
using Gamification.Core.Enums;
using Gamification.Core.ResultProviders;
using Moq;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Core.ResultProviders
{
    [TestFixture]
    public class StringCollectionResultProviderTests
    {
        private Mock<BaseStringCollectionConstraint> mockConstraint;
        private StringCollectionResultProvider resultProvider;

        [SetUp]
        public void SetUp()
        {
            this.mockConstraint = new Mock<BaseStringCollectionConstraint>();
            mockConstraint.Setup(x => x.GetValuesToCompare(It.IsAny<Gamer>()))
                .Returns(new[] { "1", "2", "3" });
            this.resultProvider = new StringCollectionResultProvider(mockConstraint.Object, new Gamer());
        }

        [Test]
        public void GetResult_WhenCollectionEqualityOperationIsHaveAndCollectionContainsValue_ShouldReturnTrue()
        {
            mockConstraint.Object.ValueToCompare = "2";
            mockConstraint.Object.CollectionEqualityOperation = CollectionEqualityOperations.Have;

            var result = this.resultProvider.GetResult();

            result.Should().Be.True();
        }

        [Test]
        public void GetResult_WhenCollectionEqualityOperationIsHaveAndCollectionNotContainsValue_ShouldReturnFalse()
        {
            mockConstraint.Object.ValueToCompare = "4";
            mockConstraint.Object.CollectionEqualityOperation = CollectionEqualityOperations.Have;

            var result = this.resultProvider.GetResult();

            result.Should().Be.False();
        }

        [Test]
        public void GetResult_WhenCollectionEqualityOperationIsNotHaveAndCollectionNotContainsValue_ShouldReturnTrue()
        {
            mockConstraint.Object.ValueToCompare = "4";
            mockConstraint.Object.CollectionEqualityOperation = CollectionEqualityOperations.NotHave;

            var result = this.resultProvider.GetResult();

            result.Should().Be.True();
        }

        [Test]
        public void GetResult_WhenCollectionEqualityOperationIsNotHaveAndCollectionContainsValue_ShouldReturnFalse()
        {
            mockConstraint.Object.ValueToCompare = "2";
            mockConstraint.Object.CollectionEqualityOperation = CollectionEqualityOperations.NotHave;

            var result = this.resultProvider.GetResult();

            result.Should().Be.False();
        }
    }
}
