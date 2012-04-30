using System;
using System.Web.Mvc;
using AutoMapper;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Core.Entities.Triggers;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels.Enums;
using Microsoft.Web.Mvc;

namespace Gamification.Web.Areas.ProjectsControlPanel.Controllers
{
    public class TriggersController : Controller
    {
        private readonly IRepository<ActionTrigger> triggersRepository;
        private readonly IRepository<Project> projectsRepository;
        private readonly IRepository<Achievement> achievementsRepository;
        private readonly IRepository<GamerStatus> statusesRepository;

        public TriggersController(
            IRepository<ActionTrigger> triggersRepository,
            IRepository<Project> projectsRepository,
            IRepository<Achievement> achievementsRepository,
            IRepository<GamerStatus> statusesRepository)
        {
            this.triggersRepository = triggersRepository;
            this.projectsRepository = projectsRepository;
            this.achievementsRepository = achievementsRepository;
            this.statusesRepository = statusesRepository;
        }

        public ActionResult Add(int projectId)
        {
            var project = this.projectsRepository.GetByIdIncluding(
                projectId, x => x.Achievements, x => x.Statuses);
            var viewModel = Mapper.Map<Project, ActionTriggerViewModel>(project);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(int projectId, ActionTriggerViewModel triggerViewModel)
        {
            if (!ModelState.IsValid)
                return View("Add", triggerViewModel);
            
            if (triggerViewModel.Id == 0)
            {
                ActionTrigger actionTrigger;
                switch (triggerViewModel.TriggersTypes)
                {
                    case TriggersTypes.Achievement:
                        actionTrigger = CreateAchievementTrigger(triggerViewModel);
                        break;
                    case TriggersTypes.ChangePoints:
                        actionTrigger = CreateChangePointsTrigger(triggerViewModel);
                        break;
                    case TriggersTypes.AddOrRemoveStatus:
                        actionTrigger = CreateStatusTrigger(triggerViewModel);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                actionTrigger.Project = this.projectsRepository.GetById(projectId);
                this.triggersRepository.AddPhysically(actionTrigger);
            }
            else
            {

            }
            return this.RedirectToAction<ProjectsController>(x => x.Show(projectId));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var actionTrigger = this.triggersRepository.GetById(id);
            var projectId = actionTrigger.Project.Id;
            try
            {
                this.triggersRepository.DeletePhysically(actionTrigger);
                return this.RedirectToAction<ProjectsController>(x => x.Show(projectId));
            }
            catch
            {
                return this.RedirectToAction<ProjectsController>(x => x.Show(projectId));
            }
        }

        public AchievementsTrigger CreateAchievementTrigger(ActionTriggerViewModel viewModel)
        {
            var achievementTrigger = Mapper.Map<ActionTriggerViewModel, AchievementsTrigger>(viewModel);
            var achievement = this.achievementsRepository.GetById(viewModel.Achievements.Id);
            achievementTrigger.Achievement = achievement;
            return achievementTrigger;
        }

        public AddOrRemoveStatusTrigger CreateStatusTrigger(ActionTriggerViewModel viewModel)
        {
            var statusTrigger = Mapper.Map<ActionTriggerViewModel, AddOrRemoveStatusTrigger>(viewModel);
            statusTrigger.Status = this.statusesRepository.GetById(viewModel.Statuses.Id);
            return statusTrigger;
        }

        public ChangePointsTrigger CreateChangePointsTrigger(ActionTriggerViewModel viewModel)
        {
            var pointsTrigger = Mapper.Map<ActionTriggerViewModel, ChangePointsTrigger>(viewModel);
            return pointsTrigger;
        }
    }
}
