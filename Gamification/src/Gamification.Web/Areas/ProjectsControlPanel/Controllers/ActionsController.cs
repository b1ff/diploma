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
    }
}