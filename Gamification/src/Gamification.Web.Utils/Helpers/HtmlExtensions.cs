using System.Web.Mvc;
using Gamification.Core;

namespace Gamification.Web.Utils.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString AddClassIfTrue(this HtmlHelper html, bool condition, string className)
        {
            Guard.StringArgumentIsNullOrBlank(className, "className");
            if (condition)
                return new MvcHtmlString(string.Format("class=\"{0}\"", className));

            return MvcHtmlString.Empty;
        }
    }
}
