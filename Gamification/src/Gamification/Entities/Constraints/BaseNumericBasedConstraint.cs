using Gamification.Core.Enums;
using Gamification.Core.ResultProviders;

namespace Gamification.Core.Entities.Constraints
{
    public abstract class BaseNumericBasedConstraint : BaseConstraint<double>
    {
        public BooleanOperations BooleanOperation
        {
            get { return (BooleanOperations)BooleanOperationId; }
            set { BooleanOperationId = (byte)value; }
        }

        public byte BooleanOperationId { get; set; }

        public abstract double GetValueToCompare(Gamer gamer);
        
        public override BooleanResultProvider GetResultProvider(Gamer gamer)
        {
            return new QuantityBasedResultsProvider(this, gamer);
        }
    }
}