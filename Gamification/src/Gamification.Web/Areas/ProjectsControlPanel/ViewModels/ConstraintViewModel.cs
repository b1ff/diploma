using System.ComponentModel.DataAnnotations;
using Gamification.Core.Enums;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels.Enums;
using Gamification.Web.Utils.CommonViewModels;

namespace Gamification.Web.Areas.ProjectsControlPanel.ViewModels
{
    public class ConstraintViewModel
    {
        public ConstraintTypes ConstraintType { get; set; }

        public CollectionEqualityOperations CollectionOperation { get; set; }

        public BooleanOperations BooleanOperation { get; set; }

        [Required]
        public string Description { get; set; }

        public DataSource Achievements { get; set; }

        public DataSource Statuses { get; set; }

        public long? Points { get; set; }

        public int? Level { get; set; }

        public int ProjectId { get; set; }
    }
}