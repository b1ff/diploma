using Gamification.Core.Enums;

namespace Gamification.Core.Entities.Triggers
{
    public class AchievementsTrigger : ActionTrigger
    {
        public Achievement Achievement { get; set; }

        public AssignUnassign ActionWithAchievement
        {
            get { return (AssignUnassign)ActionWithAchievementId; }
            set { ActionWithAchievementId = (byte)value; }
        }

        public byte ActionWithAchievementId { get; set; }

        protected override void RaiseTriggerAction(Gamer gamer)
        {
            if (ActionWithAchievement == AssignUnassign.Assign)
            {
                gamer.Achievements.Add(Achievement);
            }
            else
            {
                if (gamer.Achievements.Contains(Achievement))
                    gamer.Achievements.Remove(Achievement);
            }
        }
    }
}