using System;
using System.Linq.Expressions;
using Gamification.Core.Entities;
using LinqSpecs;

namespace Gamification.Core.Specifications
{
    public class GamerByNameSpec : Specification<Gamer>
    {
        private readonly string gamerName;

        public GamerByNameSpec(string gamerName)
        {
            this.gamerName = gamerName;
        }

        public override Expression<Func<Gamer, bool>> IsSatisfiedBy()
        {
            return x => x.UniqueKey == gamerName;
        }
    }
}