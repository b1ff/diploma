using System.Linq;
using System.Web.Mvc;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels;
using Gamification.Web.AutoMapper.Extensions;
using Gamification.Web.Utils.Helpers;
using Microsoft.Web.Mvc;

namespace Gamification.Web.Areas.ProjectsControlPanel.Controllers
{
    [Authorize]
    [ActionLinkArea("ProjectsControlPanel")]
    public class ProjectsController : Controller
    {
        private readonly IUsersRepository usersRepository;
        private readonly IRepository<Project> projectsRepository;

        public ProjectsController(
            IUsersRepository usersRepository,
            IRepository<Project> projectsRepository)
        {
            this.usersRepository = usersRepository;
            this.projectsRepository = projectsRepository;
        }

        public ActionResult Index()
        {
            var currentUser = this.usersRepository.GetCurrentUser();
            var projects = projectsRepository.GetAll().Where(x => x.User.Id == currentUser.Id).ToList();
            return View(projects);
        }

        public ActionResult ProjectsNav()
        {
            var currentUser = this.usersRepository.GetCurrentUser();
            if (currentUser == null)
            {
                return PartialView(Enumerable.Empty<ProjectNavItemViewModel>());
            }

            var navItems = this.MapEnumerable<Project, ProjectNavItemViewModel>(currentUser.Projects);
            return PartialView(navItems);
        }

        public ActionResult Show(int id)
        {
            var project = this.projectsRepository.GetById(id);
            return View(project);
        }

        [HttpPost]
        public ActionResult Save(int? projectId, string title)
        {
            var currentUser = this.usersRepository.GetCurrentUser();
            var existingProject = this.projectsRepository.FirstOrDefault(x => x.Title == title && x.User.Id == currentUser.Id);
            if (existingProject != null)
            {
                ModelState.AddGeneralError("Project with the same name alredy exist.");
                return this.RedirectToAction(x => x.Index());
            }

            Project project;

            if (!projectId.HasValue)
            {
                project = new Project();
                project.Title = title;
                this.projectsRepository.Add(project);
            }
            else
            {
                project = this.projectsRepository.GetById(projectId.Value);
                project.Title = title;
            }

            this.projectsRepository.SaveChanges();
            return this.RedirectToAction(x => x.Show(project.Id));
        }
    }
}
