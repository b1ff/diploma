using System.ComponentModel.DataAnnotations;
using Gamification.Core.Enums;
using Gamification.Core.Operations;
using Gamification.Core.ResultProviders;

namespace Gamification.Core.Entities.Constraints
{
    public abstract class BaseQuantityBasedConstraint : BaseConstraint<double>
    {
        [NotMapped]
        public BooleanOperations BooleanOperation
        {
            get { return (BooleanOperations)BooleanOperationId; }
            set { BooleanOperationId = (byte)value; }
        }

        [Column("BooleanOperation")]
        public byte BooleanOperationId { get; set; }

        public abstract double GetValueToCompare(Gamer gamer);
        
        public override bool GetResult(Gamer gamer)
        {
            return this.GetResultProvider(gamer).GetResult();
        }

        public override IBooleanResultProvider GetResultProvider(Gamer gamer)
        {
            return new QuantityBasedResultsProvider(this, gamer);
        }
    }
}