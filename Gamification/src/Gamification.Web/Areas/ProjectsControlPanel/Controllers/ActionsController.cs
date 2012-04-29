using System.Linq;
using System.Web.Mvc;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Microsoft.Web.Mvc;

namespace Gamification.Web.Areas.ProjectsControlPanel.Controllers
{
    [Authorize]
    [ActionLinkArea("ProjectsControlPanel")]
    public class ActionsController : Controller
    {
        private readonly IRepository<GameAction> actionsRepository;

        public ActionsController(IRepository<GameAction> actionsRepository)
        {
            this.actionsRepository = actionsRepository;
        }

        public ActionResult Show(int id)
        {
            var action = this.actionsRepository.GetById(id);
            return View(action);
        }

        public ActionResult Index(int projectId)
        {
            var actions = this.actionsRepository
                .Where(x => x.Project.Id == projectId).ToList();

            return View(actions);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(GameAction action)
        {
            if (!ModelState.IsValid)
                return View("Add", action);

            this.actionsRepository.AddPhysically(action);
            return this.RedirectToAction(x => x.Show(action.Id));
        }
    }
}