using System.ComponentModel.DataAnnotations;
using Gamification.Core.Enums;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels.Enums;
using Gamification.Web.Utils.CommonViewModels;

namespace Gamification.Web.Areas.ProjectsControlPanel.ViewModels
{
    public class ActionTriggerViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public TriggersTypes TriggersTypes { get; set; }

        public DataSource Achievements { get; set; }

        public AssignUnassign AchievementAction { get; set; }

        public DataSource Status { get; set; }

        public AssignUnassign StatusAction { get; set; }

        public PointsOperation PointsOperation { get; set; }

        public int? Points { get; set; }
    }
}