using System.Linq;
using System.Web.Mvc;
using Gamification.Core.Extensions;
using Microsoft.Web.Mvc;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Web.Utils.Helpers;

namespace Gamification.Web.Areas.ProjectsControlPanel.Controllers
{
    [Authorize]
    [ActionLinkArea("ProjectsControlPanel")]
    public class StatusesController : Controller
    {
        private readonly IRepository<GamerStatus> statusesRepository;
        private readonly IRepository<Project> projectsRepository;

        public StatusesController(
            IRepository<GamerStatus> statusesRepository,
            IRepository<Project> projectsRepository)
        {
            this.statusesRepository = statusesRepository;
            this.projectsRepository = projectsRepository;
        }

        [HttpPost]
        public ActionResult Save(int projectId, string statusTitle)
        {
            if (statusTitle.IsNullOrBlank())
            {
                ModelState.AddGeneralError("Status cannot be empty");
                return this.RedirectToAction<ProjectsController>(x => x.Show(projectId));
            }

            var isExistStatus =
                this.statusesRepository.Any(x => x.UserProject.Id == projectId && x.StatusName == statusTitle);

            if (isExistStatus)
            {
                ModelState.AddGeneralError(string.Format("Status \"{0}\" already exist", statusTitle));
                return this.RedirectToAction<ProjectsController>(x => x.Show(projectId));
            }

            var project = this.projectsRepository.StrictGetById(projectId);
            var status = new GamerStatus();
            status.UserProject = project;
            status.StatusName = statusTitle;
            this.statusesRepository.AddPhysically(status);
            return this.RedirectToAction<ProjectsController>(x => x.Show(projectId));
        }
    }
}
