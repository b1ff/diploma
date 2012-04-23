using System;
using Gamification.Core.Enums;

namespace Gamification.Core.ResultProviders
{
    public static class BooleanOperationExtension
    {
        public static bool Calculate(this BooleanOperations operation, double firstNum, double secondNum)
        {
            const double epsilon = 0.0001;

            switch (operation)
            {
                case BooleanOperations.Equals:
                    return Math.Abs(firstNum - secondNum) < epsilon;
                case BooleanOperations.GreaterThan:
                    return firstNum > secondNum;
                case BooleanOperations.LessThan:
                    return firstNum < secondNum;
                case BooleanOperations.NotEqual:
                    return Math.Abs(firstNum - secondNum) > epsilon;
                case BooleanOperations.GreaterOrEquals:
                    return firstNum >= secondNum;
                case BooleanOperations.LessOrEquals:
                    return firstNum <= secondNum;
                default:
                    throw new ArgumentOutOfRangeException("operation");
            }
        }
    }
}
