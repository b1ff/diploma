using Gamification.Core.Operations;

namespace Gamification.Core.Entities.Constraints
{
    public interface IConstraint<TValue>
    {
        string Description { get; set; }
        TValue ValueToCompare { get; set; }
        bool GetResult(Gamer gamer);
        IBooleanResultProvider GetResultProvider(Gamer gamer);
    }
}