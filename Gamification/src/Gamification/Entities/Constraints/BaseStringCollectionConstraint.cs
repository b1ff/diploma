using System.Collections.Generic;
using Gamification.Core.Enums;
using Gamification.Core.ResultProviders;

namespace Gamification.Core.Entities.Constraints
{
    public abstract class BaseStringCollectionConstraint : BaseConstraint<string>
    {
        public CollectionEqualityOperations CollectionEqualityOperation
        {
            get { return (CollectionEqualityOperations) CollectionEqualityOperationId; }
            set { CollectionEqualityOperationId = (byte) value; }
        }

        public byte CollectionEqualityOperationId { get; set; }

        public override BooleanResultProvider GetResultProvider(Gamer gamer)
        {
            return new StringCollectionResultProvider(this, gamer);
        }

        public abstract IEnumerable<string> GetValuesToCompare(Gamer gamer);
    }
}