using Gamification.Core.ResultProviders;

namespace Gamification.Core.Entities.Constraints
{
    public interface IConstraint<TValue>
    {
        string Description { get; set; }
        TValue ValueToCompare { get; set; }
        bool GetResult(Gamer gamer);
        BooleanResultProvider GetResultProvider(Gamer gamer);
    }
}