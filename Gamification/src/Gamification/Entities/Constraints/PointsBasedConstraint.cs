namespace Gamification.Core.Entities.Constraints
{
    public class PointsBasedConstraint : BaseQuantityBasedConstraint
    {
        public override double GetValueToCompare(Gamer gamer)
        {
            return gamer.Points;
        }
    }
}