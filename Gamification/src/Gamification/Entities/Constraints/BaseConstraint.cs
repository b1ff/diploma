using Gamification.Core.Operations;

namespace Gamification.Core.Entities.Constraints
{
    public abstract class BaseConstraint<TValue> : BaseEntity, IConstraint<TValue>
    {
        public string Description { get; set; }
        public TValue ValueToCompare { get; set; }
        public abstract bool GetResult(Gamer gamer);
        public abstract IBooleanResultProvider GetResultProvider(Gamer gamer);
    }
}
