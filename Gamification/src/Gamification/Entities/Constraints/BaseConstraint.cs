using Gamification.Core.ResultProviders;

namespace Gamification.Core.Entities.Constraints
{
    public abstract class BaseConstraint<TValue> : BaseEntity, IConstraint<TValue>
    {
        public string Description { get; set; }
        public TValue ValueToCompare { get; set; }
        public abstract BooleanResultProvider GetResultProvider(Gamer gamer);
        public Project Project { get; set; }

        public bool GetResult(Gamer gamer)
        {
            return this.GetResultProvider(gamer).GetResult();
        }
    }
}
