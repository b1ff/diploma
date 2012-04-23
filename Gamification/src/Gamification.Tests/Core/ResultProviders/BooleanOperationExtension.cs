using Gamification.Core.Enums;
using Gamification.Core.ResultProviders;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Core.ResultProviders
{
    [TestFixture]
    public class BooleanOperationExtension
    {
        [TestCase(BooleanOperations.Equals, 1, 1, true)]
        [TestCase(BooleanOperations.Equals, 1, 2, false)]
        [TestCase(BooleanOperations.NotEqual, 1, 2, true)]
        [TestCase(BooleanOperations.NotEqual, 1, 1, false)]
        [TestCase(BooleanOperations.LessThan, 1, 2, true)]
        [TestCase(BooleanOperations.LessThan, 2, 1, false)]
        [TestCase(BooleanOperations.LessThan, 2, 2, false)]
        [TestCase(BooleanOperations.GreaterThan, 2, 1, true)]
        [TestCase(BooleanOperations.GreaterThan, 1, 2, false)]
        [TestCase(BooleanOperations.GreaterThan, 2, 2, false)]
        [TestCase(BooleanOperations.LessOrEquals, 1, 1, true)]
        [TestCase(BooleanOperations.LessOrEquals, 1, 2, true)]
        [TestCase(BooleanOperations.LessOrEquals, 2, 1, false)]
        [TestCase(BooleanOperations.GreaterOrEquals, 1, 1, true)]
        [TestCase(BooleanOperations.GreaterOrEquals, 2, 1, true)]
        [TestCase(BooleanOperations.GreaterOrEquals, 1, 2, false)]
        public void Calculate_ShouldCalculateBooleanOperationForTwoDigitsRight(
            BooleanOperations operation, double num1, double num2, bool expectedResult)
        {
            var result = operation.Calculate(num1, num2);

            result.Should().Be.EqualTo(expectedResult);
        }
    }
}
