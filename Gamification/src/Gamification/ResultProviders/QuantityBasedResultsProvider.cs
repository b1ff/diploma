using Gamification.Core.Entities;
using Gamification.Core.Entities.Constraints;

namespace Gamification.Core.ResultProviders
{
    public class QuantityBasedResultsProvider : BaseGamerDependResultProvider
    {
        private readonly BaseNumericBasedConstraint numericBasedConstraint;

        public QuantityBasedResultsProvider(BaseNumericBasedConstraint numericBasedConstraint, Gamer gamer) : base(gamer)
        {
            this.numericBasedConstraint = numericBasedConstraint;
        }

        public override bool GetResult()
        {
            var propertyValue = this.numericBasedConstraint.GetValueToCompare(this.Gamer);
            return this.numericBasedConstraint.BooleanOperation.Calculate(propertyValue, this.numericBasedConstraint.ValueToCompare);
        }
    }
}