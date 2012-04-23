using System.Collections.Generic;
using System.Linq;

namespace Gamification.Core.Entities.Constraints
{
    public class StatusConstraint : BaseStringCollectionConstraint
    {
        public override IEnumerable<string> GetValuesToCompare(Gamer gamer)
        {
            return gamer.GamerStatuses.Select(x => x.StatusName).ToList();
        }
    }
}