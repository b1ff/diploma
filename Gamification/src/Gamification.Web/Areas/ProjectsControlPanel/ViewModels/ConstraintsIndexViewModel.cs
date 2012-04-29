using System.Collections.Generic;
using Gamification.Core.Entities.Constraints;

namespace Gamification.Web.Areas.ProjectsControlPanel.ViewModels
{
    public class ConstraintsIndexViewModel
    {
        public IEnumerable<BaseNumericBasedConstraint> NumericConstraints { get; set; }
        public IEnumerable<BaseStringCollectionConstraint> StringsConstraits { get; set; } 
    }
}