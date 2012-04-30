using System.Web;
using System.Web.Mvc;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Core.Extensions;
using Gamification.Core.StorageManagement;
using Gamification.Web.Utils.CommonViewModels;
using Gamification.Web.Utils.Helpers;
using Gamification.Web.Utils.WebStorage;
using Microsoft.Web.Mvc;

namespace Gamification.Web.Areas.ProjectsControlPanel.Controllers
{
    [Authorize]
    [ActionLinkArea("ProjectsControlPanel")]
    public class AchievementsController : Controller
    {
        private readonly IRepository<Achievement> achievementsRepository;
        private readonly IRepository<Project> projectsRepository;
        private readonly IStorage storage;

        public AchievementsController(
            IRepository<Achievement> achievementsRepository, 
            IRepository<Project> projectsRepository,
            IStorage storage)
        {
            this.achievementsRepository = achievementsRepository;
            this.projectsRepository = projectsRepository;
            this.storage = storage;
        }

        public ActionResult Add(int projectId)
        {
            return View();
        }

        public ActionResult Save(int projectId, string achievementTitle, FileViewModel achievementImage)
        {
            if (achievementImage == null)
            {
                ModelState.AddGeneralError("Achievement file required");
                return this.RedirectToAction<ProjectsController>(x => x.Index());
            }

            if (achievementTitle.IsNullOrBlank())
            {
                ModelState.AddGeneralError("Achievement title required");
                return this.RedirectToAction<ProjectsController>(x => x.Index());
            }

            var achievement = new Achievement();
            var project = this.projectsRepository.GetById(projectId);
            achievement.Project = project;
            achievement.Name = achievementTitle;
            achievementsRepository.AddPhysically(achievement);

            achievement.ImageName = storage.SaveAchievementImage(achievementImage.InputStream, achievementImage.Name);
            this.achievementsRepository.SaveChanges();

            return this.RedirectToAction<ProjectsController>(x => x.Show(projectId));
        }
    }
}
