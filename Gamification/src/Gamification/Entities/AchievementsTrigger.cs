namespace Gamification.Core.Entities
{
    public class AchievementsTrigger : ActionTrigger
    {
        public Achievement AchievementToAssign { get; set; }

        public override void TriggerOnGamer(Gamer gamer)
        {
            gamer.Achievements.Add(AchievementToAssign);
        }
    }
}