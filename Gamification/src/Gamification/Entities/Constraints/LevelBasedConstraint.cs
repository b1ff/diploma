namespace Gamification.Core.Entities.Constraints
{
    public class LevelBasedConstraint : BaseQuantityBasedConstraint
    {
        public override double GetValueToCompare(Gamer gamer)
        {
            return gamer.CurrentLevel.LevelNumber;
        }
    }
}