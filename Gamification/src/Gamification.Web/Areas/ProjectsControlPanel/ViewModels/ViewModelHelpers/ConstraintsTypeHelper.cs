using System;
using Gamification.Core.Entities.Constraints;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels.Enums;

namespace Gamification.Web.Areas.ProjectsControlPanel.ViewModels.ViewModelHelpers
{
    public static class ConstraintsTypeHelper
    {
        public static ConstraintTypes ConstraintType<TConstraint>(this TConstraint constraint)
            where TConstraint : IConstraint
        {
            if (constraint is AchievementsConstraint)
            {
                return ConstraintTypes.AchievementConstraint;
            }

            if (constraint is LevelBasedConstraint)
            {
                return ConstraintTypes.LevelBasedConstraint;
            }

            if (constraint is PointsBasedConstraint)
            {
                return ConstraintTypes.PointsBasedConstraint;
            }

            if (constraint is StatusConstraint)
            {
                return ConstraintTypes.StatusConstraint;
            }

            throw new ArgumentException("Constraint type not found", "constraint");
        }
    }
}