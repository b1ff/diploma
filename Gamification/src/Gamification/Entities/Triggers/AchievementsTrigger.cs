namespace Gamification.Core.Entities.Triggers
{
    public class AchievementsTrigger : ActionTrigger
    {
        public Achievement AchievementToAssign { get; set; }

        protected override void RaiseTriggerAction(Gamer gamer)
        {
            gamer.Achievements.Add(AchievementToAssign);
        }
    }
}