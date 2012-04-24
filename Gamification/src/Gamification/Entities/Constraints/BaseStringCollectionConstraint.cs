using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gamification.Core.Enums;
using Gamification.Core.Operations;
using Gamification.Core.ResultProviders;

namespace Gamification.Core.Entities.Constraints
{
    public abstract class BaseStringCollectionConstraint : BaseConstraint<string>
    {
        [NotMapped]
        public CollectionEqualityOperations CollectionEqualityOperation
        {
            get { return (CollectionEqualityOperations) CollectionEqualityOperationId; }
            set { CollectionEqualityOperationId = (byte) value; }
        }

        [Column("CollectionEqualityOperation")]
        public byte CollectionEqualityOperationId { get; set; }

        public override bool GetResult(Gamer gamer)
        {
            return this.GetResultProvider(gamer).GetResult();
        }

        public override IBooleanResultProvider GetResultProvider(Gamer gamer)
        {
            return new StringCollectionResultProvider(this, gamer);
        }

        public abstract IEnumerable<string> GetValuesToCompare(Gamer gamer);
    }
}