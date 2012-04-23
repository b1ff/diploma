using System.ComponentModel.DataAnnotations;
using Gamification.Core.Enums;
using Gamification.Core.Operations;
using Gamification.Core.ResultProviders;

namespace Gamification.Core.Entities.Constraints
{
    public abstract class BaseQuantityBasedConstraint : BaseEntity, IOneValueConstraint<double>
    {
        [NotMapped]
        public BooleanOperations BooleanOperation { 
            get { return (BooleanOperations) BooleanOperationId; } 
            set { BooleanOperationId = (int) value; } }

        [Column("BooleanOperation")]
        public int BooleanOperationId { get; set; }

        public string Description { get; set; }

        public double ValueToCompare { get; set; }

        public abstract double GetValueToCompare(Gamer gamer);
        
        public bool GetResult(Gamer gamer)
        {
            return this.GetResultProvider(gamer).GetResult();
        }

        public IBooleanResultProvider GetResultProvider(Gamer gamer)
        {
            return new QuantityBasedResultsProvider(this, gamer);
        }
    }
}