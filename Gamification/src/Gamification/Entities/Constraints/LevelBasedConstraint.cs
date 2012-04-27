namespace Gamification.Core.Entities.Constraints
{
    public class LevelBasedConstraint : BaseNumericBasedConstraint
    {
        public override double GetValueToCompare(Gamer gamer)
        {
            return gamer.CurrentLevel.LevelNumber;
        }
    }
}