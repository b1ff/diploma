using System;
using Gamification.Core.Enums;

namespace Gamification.Core.Operations
{
    public static class BooleanOperationExtension
    {
        public static bool Calculate(this BooleanOperation operation, double firstNum, double secondNum)
        {
            const double epsilon = 0.0001;

            switch (operation)
            {
                case BooleanOperation.Equals:
                    return Math.Abs(firstNum - secondNum) < epsilon;
                case BooleanOperation.GreaterThan:
                    return firstNum > secondNum;
                case BooleanOperation.LessThan:
                    return firstNum < secondNum;
                case BooleanOperation.NotEqual:
                    return Math.Abs(firstNum - secondNum) > epsilon;
                case BooleanOperation.GreaterOrEquals:
                    return firstNum >= secondNum;
                case BooleanOperation.LessOrEquals:
                    return firstNum <= secondNum;
                default:
                    throw new ArgumentOutOfRangeException("operation");
            }
        }
    }
}
