namespace Gamification.Core.Entities
{
    public class AchievementsTrigger : ActionTrigger
    {
        public Achievement AchievementToAssign { get; set; }

        public override void CallOnGamer(Gamer gamer)
        {
            gamer.Achievements.Add(AchievementToAssign);
        }
    }
}