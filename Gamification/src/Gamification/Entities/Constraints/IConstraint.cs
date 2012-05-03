using Gamification.Core.ResultProviders;

namespace Gamification.Core.Entities.Constraints
{
    public interface IConstraint<TValue> : IConstraint
    {
        string Description { get; set; }
        TValue ValueToCompare { get; set; }
    }

    public interface IConstraint
    {
        bool GetResult(Gamer gamer);
        BooleanResultProvider GetResultProvider(Gamer gamer);
    }
}