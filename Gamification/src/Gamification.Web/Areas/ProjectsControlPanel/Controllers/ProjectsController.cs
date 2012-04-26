using System.Web.Mvc;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels;
using Gamification.Web.AutoMapper.Extensions;
using Microsoft.Web.Mvc;

namespace Gamification.Web.Areas.ProjectsControlPanel.Controllers
{
    [Authorize]
    [ActionLinkArea("ProjectsControlPanel")]
    public class ProjectsController : Controller
    {
        private readonly IUsersRepository usersRepository;

        public ProjectsController(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProjectsNav()
        {
            var currentUser = this.usersRepository.GetCurrentUser();
            var navItems = this.MapEnumerable<Project, ProjectNavItemViewModel>(currentUser.Projects);
            return PartialView(navItems);
        }

        public ActionResult Show(int id)
        {
            return View();
        }
    }
}
