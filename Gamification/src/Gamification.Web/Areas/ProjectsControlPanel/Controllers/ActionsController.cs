using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Core.Entities.Constraints;
using Gamification.Core.Entities.Triggers;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels;
using Microsoft.Web.Mvc;

namespace Gamification.Web.Areas.ProjectsControlPanel.Controllers
{
    [Authorize]
    [ActionLinkArea("ProjectsControlPanel")]
    public class ActionsController : Controller
    {
        private readonly IRepository<GameAction> actionsRepository;
        private readonly IRepository<Project> projectsRepository;
        private readonly IRepository<ActionTrigger> triggersRepository;
        private readonly IRepository<BaseNumericBasedConstraint> qtyBasedRepository;
        private readonly IRepository<BaseStringCollectionConstraint> collectionConstraintRepository;

        public ActionsController(
            IRepository<GameAction> actionsRepository,
            IRepository<Project> projectsRepository,
            IRepository<ActionTrigger> triggersRepository,
            IRepository<BaseNumericBasedConstraint> qtyBasedRepository,
            IRepository<BaseStringCollectionConstraint> collectionConstraintRepository)
        {
            this.actionsRepository = actionsRepository;
            this.projectsRepository = projectsRepository;
            this.triggersRepository = triggersRepository;
            this.qtyBasedRepository = qtyBasedRepository;
            this.collectionConstraintRepository = collectionConstraintRepository;
        }

        public ActionResult Add(int projectId)
        {
            var project = this.projectsRepository.GetById(projectId,
                x => x.CollectionConstraints, x => x.NumericConstraints, x => x.Triggers);
            var viewModel = Mapper.Map<GameActionViewModel>(project);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(int projectId, GameActionViewModel actionViewModel)
        {
            if (!ModelState.IsValid)
                return View("Add", actionViewModel);

            var action = Mapper.Map<GameActionViewModel, GameAction>(actionViewModel);

            if (actionViewModel.QtyBasedConstraints != null)
                action.QtyBasedConstraints.Add(this.qtyBasedRepository.GetById(actionViewModel.QtyBasedConstraints.Id));
            if (actionViewModel.CollectionBasedConstrains != null)
                action.StringCollectionConstraints.Add(this.collectionConstraintRepository.GetById(actionViewModel.CollectionBasedConstrains.Id));

            action.TriggersToCall.Add(this.triggersRepository.GetById(actionViewModel.Triggers.Id));
            action.Project = this.projectsRepository.GetById(projectId);

            this.actionsRepository.AddPhysically(action);
            return this.RedirectToAction<ProjectsController>(x => x.Show(projectId));
        }
    }
}