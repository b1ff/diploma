using System.Collections.Generic;
using System.Linq;

namespace Gamification.Core.Entities.Constraints
{
    public class AchievementsConstraint : BaseStringCollectionConstraint
    {
        public override IEnumerable<string> GetValuesToCompare(Gamer gamer)
        {
            return gamer.Achievements.Select(x => x.Name);
        }
    }
}