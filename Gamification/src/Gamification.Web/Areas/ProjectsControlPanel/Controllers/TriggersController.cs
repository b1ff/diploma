using System.Linq;
using System.Web.Mvc;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities.Triggers;
using Gamification.Web.Utils.Helpers;
using Microsoft.Web.Mvc;

namespace Gamification.Web.Areas.ProjectsControlPanel.Controllers
{
    public class TriggersController : Controller
    {
        private readonly IRepository<ActionTrigger> actionRepository;

        public TriggersController(IRepository<ActionTrigger> actionRepository)
        {
            this.actionRepository = actionRepository;
        }

        public ActionResult Index(int projectId)
        {
            var triggers = this.actionRepository.Where(x => x.Project.Id == projectId).ToList();

            return View(triggers);
        }

        public ActionResult Show(int id)
        {
            var trigger = this.actionRepository.GetById(id);
            trigger.ThrowNotFoundIfNull();
            return View(trigger);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var actionTrigger = this.actionRepository.GetById(id);
                var projectId = actionTrigger.Project.Id;
                this.actionRepository.DeletePhysically(actionTrigger);

                return this.RedirectToAction(x => x.Index(projectId));
            }
            catch
            {
                return this.RedirectToAction(x => x.Show(id));
            }
        }
    }
}
