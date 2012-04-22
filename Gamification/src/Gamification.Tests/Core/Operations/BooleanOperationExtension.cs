using Gamification.Core.Enums;
using Gamification.Core.Operations;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Core.Operations
{
    [TestFixture]
    public class BooleanOperationExtension
    {
        [TestCase(BooleanOperation.Equals, 1, 1, true)]
        [TestCase(BooleanOperation.Equals, 1, 2, false)]
        [TestCase(BooleanOperation.NotEqual, 1, 2, true)]
        [TestCase(BooleanOperation.NotEqual, 1, 1, false)]
        [TestCase(BooleanOperation.LessThan, 1, 2, true)]
        [TestCase(BooleanOperation.LessThan, 2, 1, false)]
        [TestCase(BooleanOperation.LessThan, 2, 2, false)]
        [TestCase(BooleanOperation.GreaterThan, 2, 1, true)]
        [TestCase(BooleanOperation.GreaterThan, 1, 2, false)]
        [TestCase(BooleanOperation.GreaterThan, 2, 2, false)]
        [TestCase(BooleanOperation.LessOrEquals, 1, 1, true)]
        [TestCase(BooleanOperation.LessOrEquals, 1, 2, true)]
        [TestCase(BooleanOperation.LessOrEquals, 2, 1, false)]
        [TestCase(BooleanOperation.GreaterOrEquals, 1, 1, true)]
        [TestCase(BooleanOperation.GreaterOrEquals, 2, 1, true)]
        [TestCase(BooleanOperation.GreaterOrEquals, 1, 2, false)]
        public void Calculate_ShouldCalculateBooleanOperationForTwoDigitsRight(
            BooleanOperation operation, double num1, double num2, bool expectedResult)
        {
            var result = operation.Calculate(num1, num2);

            result.Should().Be.EqualTo(expectedResult);
        }
    }
}
