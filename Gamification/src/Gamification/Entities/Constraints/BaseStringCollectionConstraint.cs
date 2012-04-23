using System.Collections.Generic;
using Gamification.Core.Enums;
using Gamification.Core.Operations;
using Gamification.Core.ResultProviders;

namespace Gamification.Core.Entities.Constraints
{
    public abstract class BaseStringCollectionConstraint : BaseEntity, ICollectionConstraint<string>
    {
        public CollectionEqualityOperations CollectionEqualityOperation { get; set; }

        public string Description { get; set; }
        
        public string ValueToCompare { get; set; }

        public bool GetResult(Gamer gamer)
        {
            return this.GetResultProvider(gamer).GetResult();
        }

        public IBooleanResultProvider GetResultProvider(Gamer gamer)
        {
            return new StringCollectionResultProvider(this, gamer);
        }

        public abstract IEnumerable<string> GetValuesToCompare(Gamer gamer);
    }
}