namespace Gamification.Core.Entities.Triggers
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