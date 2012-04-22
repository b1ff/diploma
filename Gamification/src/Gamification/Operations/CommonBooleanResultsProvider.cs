using Gamification.Core.Entities;

namespace Gamification.Core.Operations
{
    public class CommonBooleanResultsProvider : IBooleanResultProvider
    {
        private readonly CommonBooleanConstraint booleanConstraint;
        private readonly object objectWithValueToCompare;

        public CommonBooleanResultsProvider(CommonBooleanConstraint booleanConstraint, object objectWithValueToCompare)
        {
            this.booleanConstraint = booleanConstraint;
            this.objectWithValueToCompare = objectWithValueToCompare;
        }

        public bool GetResult()
        {
            var propertyValue = this.booleanConstraint.GetPropertyValueToCompare(this.objectWithValueToCompare);
            return this.booleanConstraint.BooleanOperation.Calculate(propertyValue, this.booleanConstraint.ValueToCompare);
        }
    }
}