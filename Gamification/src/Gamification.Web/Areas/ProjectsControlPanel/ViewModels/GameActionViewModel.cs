using System.ComponentModel.DataAnnotations;
using Gamification.Web.Utils.CommonViewModels;

namespace Gamification.Web.Areas.ProjectsControlPanel.ViewModels
{
    public class GameActionViewModel
    {
        public DataSource Triggers { get; set; }

        public NullableDataSource QtyBasedConstraints { get; set; }

        public NullableDataSource CollectionBasedConstrains { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }
    }
}