using System.Linq;
using Gamification.Core.Entities;

namespace Gamification.WebServices.Helpers
{
    public static class EntityHelpers
    {
        public static Gamer CreateGamer(string gamerName, Project project)
        {
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