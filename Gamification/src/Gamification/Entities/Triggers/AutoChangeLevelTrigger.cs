using System.Linq;

namespace Gamification.Core.Entities.Triggers
{
    public class AutoChangeLevelTrigger : ActionTrigger
    {
        protected override void RaiseTriggerAction(Gamer gamer)
        {
            if (!gamer.Project.UsePoints)
                return;

            if (gamer.Points < gamer.CurrentLevel.NeededPoints)
            {
                var levelToAssign = gamer.Project.Levels.OrderBy(x => x.LevelNumber)
                    .FirstOrDefault(x => x < gamer.CurrentLevel);
                gamer.CurrentLevel = levelToAssign;
            }
            else
            {
                var levelToAssign = gamer.Project.Levels.OrderByDescending(level => level.LevelNumber)
                    .FirstOrDefault(level => level > gamer.CurrentLevel && level.NeededPoints < gamer.Points);
                if (levelToAssign != null)
                    gamer.CurrentLevel = levelToAssign;
            }
        }
    }
}