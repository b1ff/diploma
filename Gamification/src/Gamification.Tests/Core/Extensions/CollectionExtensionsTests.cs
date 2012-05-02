using System.Linq;
using Gamification.Core.Extensions;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Core.Extensions
{
    [TestFixture]
    public class CollectionExtensionsTests
    {
        [Test]
        public void GetDiff_ShouldReturnItemsThatNotContainsInBothCollections()
        {
            var firstCollection = Enumerable.Range(1, 10);
            var secondCollection = Enumerable.Range(5, 10);

            var result = firstCollection.GetDiff(secondCollection);

            var expected = Enumerable.Range(1, 4).Union(Enumerable.Range(11, 4));
            result.Should().Have.SameSequenceAs(expected);
        }
    }
}
