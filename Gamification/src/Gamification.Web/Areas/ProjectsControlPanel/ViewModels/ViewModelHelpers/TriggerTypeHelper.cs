using System;
using Gamification.Core.Entities.Triggers;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels.Enums;

namespace Gamification.Web.Areas.ProjectsControlPanel.ViewModels.ViewModelHelpers
{
    public static class TriggerTypeHelper
    {
        public static TriggersTypes TriggerType<TTrigger>(this TTrigger trigger)
            where TTrigger : ActionTrigger
        {
            if (trigger is AchievementsTrigger)
            {
                return TriggersTypes.Achievement;
            }
            if (trigger is AddOrRemoveStatusTrigger)
            {
                return TriggersTypes.AddOrRemoveStatus;
            }
            if (trigger is ChangePointsTrigger)
            {
                return TriggersTypes.ChangePoints;
            }

            throw new ArgumentException("Trigger type not founded");
        }

        public static string TriggerTypeName<TTrigger>(this TTrigger trigger)
            where TTrigger : ActionTrigger
        {
            return trigger.TriggerType().ToString();
        }
    }
}