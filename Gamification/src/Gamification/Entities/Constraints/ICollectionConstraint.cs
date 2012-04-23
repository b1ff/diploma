using System.Collections.Generic;

namespace Gamification.Core.Entities.Constraints
{
    public interface ICollectionConstraint<TValue> : IConstraint<TValue>
    {
        IEnumerable<TValue> GetValuesToCompare(Gamer gamer);
    }
}