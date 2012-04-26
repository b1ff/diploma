using System.Web.Mvc;

namespace Gamification.Web.Areas.ProjectsControlPanel
{
    public class ProjectsControlPanelAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get 
            {
                return "ProjectsControlPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ProjectsControlPanel_default",
                "ProjectsControlPanel/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
