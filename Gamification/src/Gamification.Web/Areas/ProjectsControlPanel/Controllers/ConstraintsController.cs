using System;
using System.Web.Mvc;
using System.Linq;
using AutoMapper;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Core.Entities.Constraints;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels.Enums;
using Microsoft.Web.Mvc;

namespace Gamification.Web.Areas.ProjectsControlPanel.Controllers
{
    [Authorize]
    [ActionLinkArea("ProjectsControlPanel")]
    public class ConstraintsController : Controller
    {
        private readonly IRepository<BaseNumericBasedConstraint> numericConstraintsRepository;
        private readonly IRepository<BaseStringCollectionConstraint> collectionConstraintsRepository;
        private readonly IRepository<Project> projectsRepository;
        private readonly IRepository<GamerStatus> statusesRepository;
        private readonly IRepository<Achievement> achievementsRepository;
        private readonly ConstraintTypes[] quantityTypes = new[] { ConstraintTypes.PointsBasedConstraint, ConstraintTypes.LevelBasedConstraint };

        public ConstraintsController(
            IRepository<BaseNumericBasedConstraint> numericConstraintsRepository,
            IRepository<BaseStringCollectionConstraint> collectionConstraintsRepository,
            IRepository<Project> projectsRepository,
            IRepository<GamerStatus> statusesRepository,
            IRepository<Achievement> achievementsRepository)
        {
            this.numericConstraintsRepository = numericConstraintsRepository;
            this.collectionConstraintsRepository = collectionConstraintsRepository;
            this.projectsRepository = projectsRepository;
            this.statusesRepository = statusesRepository;
            this.achievementsRepository = achievementsRepository;
        }

        public ActionResult Add(int projectId)
        {
            var project = this.projectsRepository.GetById(projectId, x => x.Achievements, x => x.Statuses);
            var viewModel = Mapper.Map<Project, ConstraintViewModel>(project);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(int projectId, ConstraintViewModel constraintViewModel)
        {
            if (!ModelState.IsValid)
            {
                constraintViewModel.ProjectId = projectId;
                return View("Add", constraintViewModel);
            }

            if (quantityTypes.Contains(constraintViewModel.ConstraintType))
            {
                var constraint = this.CreateNumericConstraint(constraintViewModel);
                FillConstraint<BaseNumericBasedConstraint, double>(constraint, projectId);
            }
            else
            {
                var constraint = this.CreateCollectionConstraint(constraintViewModel);
                FillConstraint<BaseStringCollectionConstraint, string>(constraint, projectId);
            }

            this.projectsRepository.SaveChanges();
            return this.RedirectToAction<ProjectsController>(x => x.Show(projectId));
        }

        private void FillConstraint<TConstraint, TValue>(TConstraint constraint, int projectId)

            where TConstraint : BaseConstraint<TValue>
        {
            constraint.Project = this.projectsRepository.GetById(projectId);
        }

        private BaseNumericBasedConstraint CreateNumericConstraint(ConstraintViewModel viewModel)
        {
            BaseNumericBasedConstraint constraint;
            switch (viewModel.ConstraintType)
            {
                case ConstraintTypes.PointsBasedConstraint:
                    constraint = Mapper.Map<PointsBasedConstraint>(viewModel);
                    break;
                case ConstraintTypes.LevelBasedConstraint:
                    constraint = Mapper.Map<LevelBasedConstraint>(viewModel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            this.numericConstraintsRepository.Add(constraint);
            return constraint;
        }

        private BaseStringCollectionConstraint CreateCollectionConstraint(ConstraintViewModel viewModel)
        {
            BaseStringCollectionConstraint constraint;
            switch (viewModel.ConstraintType)
            {
                case ConstraintTypes.AchievementConstraint:
                    constraint = Mapper.Map<AchievementsConstraint>(viewModel);
                    constraint.ValueToCompare = this.achievementsRepository.GetById(viewModel.Achievements.Id).Name;
                    break;
                case ConstraintTypes.StatusConstraint:
                    constraint = Mapper.Map<StatusConstraint>(viewModel);
                    constraint.ValueToCompare = this.statusesRepository.GetById(viewModel.Statuses.Id).StatusName;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            this.collectionConstraintsRepository.Add(constraint);
            return constraint;
        }
    }
}
