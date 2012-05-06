using System.Linq;
using Gamification.Core.Entities;
using Gamification.Core.Extensions;

namespace Gamification.WebServices.Helpers
{
    public static class EntityHelpers
    {
        public static Gamer CreateGamer(string gamerName, Project project)
        {
            if (gamerName.IsNullOrBlank())
                return null;

            var gamer = new Gamer();
            gamer.UniqueKey = gamerName;
            gamer.Project = project;
            if (project.UseLevels)
            {
                gamer.CurrentLevel = project.Levels.Min();
            }

            return gamer;
        }
    }
}