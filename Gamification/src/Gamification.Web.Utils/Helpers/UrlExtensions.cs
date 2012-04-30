using System.Web.Mvc;
using Gamification.Core.Entities;
using Gamification.Web.Utils.WebStorage;

namespace Gamification.Web.Utils.Helpers
{
    public static class UrlExtensions
    {
        public static string Path(this UrlHelper urlHelper, Achievement achievement)
        {
            return new Storage(urlHelper).FullAchievementPath(achievement);
        }
    }
}
