using System;
using System.Data;
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
            return View();
        }

        public ActionResult ProjectsNav()
        {
            var currentUser = this.usersRepository.GetCurrentUser();
            if (currentUser == null)
            {
                return PartialView(Enumerable.Empty<ProjectNavItemViewModel>());
            }

            var currentProjects = this.projectsRepository.Where(x => x.User.Id == currentUser.Id);
            var navItems = this.MapEnumerable<Project, ProjectNavItemViewModel>(currentProjects);
            return PartialView(navItems);
        }

        public ActionResult Show(int id)
        {
            var project = this.projectsRepository.GetById(id);
            var currentUser = this.usersRepository.GetCurrentUser();
            if (!currentUser.Projects.Contains(project))
            {
                return HttpNotFound();
            }

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
                project.UserKey = Guid.NewGuid();
                project.GamerKey = Guid.NewGuid();
                project.User = currentUser;
                this.projectsRepository.Add(project);
                this.projectsRepository.SaveChanges();
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
