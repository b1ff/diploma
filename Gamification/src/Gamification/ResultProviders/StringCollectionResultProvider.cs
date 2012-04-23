using System.Linq;
using Gamification.Core.Entities;
using Gamification.Core.Entities.Constraints;
using Gamification.Core.Enums;
using Gamification.Core.Operations;

namespace Gamification.Core.ResultProviders
{
    public class StringCollectionResultProvider : BaseGamerDependResultProvider
    {
        private readonly BaseStringCollectionConstraint stringCollectionConstraint;

        public StringCollectionResultProvider(BaseStringCollectionConstraint stringCollectionConstraint, Gamer gamer) : base(gamer)
        {
            this.stringCollectionConstraint = stringCollectionConstraint;
        }

        public override bool GetResult()
        {
            var values = this.stringCollectionConstraint.GetValuesToCompare(this.Gamer);
            if (stringCollectionConstraint.CollectionEqualityOperation == CollectionEqualityOperations.Have)
            {
                return values.Contains(stringCollectionConstraint.ValueToCompare);
            }

            return !values.Contains(stringCollectionConstraint.ValueToCompare);
        }
    }
}