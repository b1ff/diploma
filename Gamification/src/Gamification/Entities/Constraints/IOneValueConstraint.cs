namespace Gamification.Core.Entities.Constraints
{
    public interface IOneValueConstraint<TValue> : IConstraint<TValue>
    {
        TValue GetValueToCompare(Gamer gamer);
    }
}