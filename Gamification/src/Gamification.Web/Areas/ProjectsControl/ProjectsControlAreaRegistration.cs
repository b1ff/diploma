using System.Web.Mvc;

namespace Gamification.Web.Areas.ProjectsControl
{
    public class ProjectsControlAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ProjectsControl";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ProjectsControl_default",
                "ProjectsControl/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
