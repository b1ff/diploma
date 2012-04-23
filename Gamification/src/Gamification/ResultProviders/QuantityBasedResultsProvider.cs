using Gamification.Core.Entities;
using Gamification.Core.Entities.Constraints;

namespace Gamification.Core.ResultProviders
{
    public class QuantityBasedResultsProvider : BaseGamerDependResultProvider
    {
        private readonly BaseQuantityBasedConstraint quantityBasedConstraint;

        public QuantityBasedResultsProvider(BaseQuantityBasedConstraint quantityBasedConstraint, Gamer gamer) : base(gamer)
        {
            this.quantityBasedConstraint = quantityBasedConstraint;
        }

        public override bool GetResult()
        {
            var propertyValue = this.quantityBasedConstraint.GetValueToCompare(this.Gamer);
            return this.quantityBasedConstraint.BooleanOperation.Calculate(propertyValue, this.quantityBasedConstraint.ValueToCompare);
        }
    }
}