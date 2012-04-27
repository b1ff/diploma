namespace Gamification.Core.Entities.Constraints
{
    public class PointsBasedConstraint : BaseNumericBasedConstraint
    {
        public override double GetValueToCompare(Gamer gamer)
        {
            return gamer.Points;
        }
    }
}