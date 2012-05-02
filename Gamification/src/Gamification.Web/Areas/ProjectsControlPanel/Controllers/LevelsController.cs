using System.Web.Mvc;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Microsoft.Web.Mvc;

namespace Gamification.Web.Areas.ProjectsControlPanel.Controllers
{
    [Authorize]
    [ActionLinkArea("ProjectsControlPanel")]
    public class LevelsController : Controller
    {
        private readonly IRepository<Project> projectsRepository;
        private readonly IRepository<Level> levelsRepository;

        public LevelsController(IRepository<Project> projectsRepository,
            IRepository<Level> levelsRepository)
        {
            this.projectsRepository = projectsRepository;
            this.levelsRepository = levelsRepository;
        }

        [HttpPost]
        public ActionResult InitializeLevels(int projectId, int levelsCount, int pointsStep)
        {
            var project = this.projectsRepository.StrictGetById(projectId, x => x.Levels);
            project.Levels.Clear();

            for (int i = 0; i < levelsCount; i++)
            {
                var level = new Level();
                level.Project = project;
                level.NeededPoints = i * pointsStep;
                this.levelsRepository.Add(level);
            }

            this.levelsRepository.SaveChanges();
            return this.RedirectToAction<ProjectsController>(x => x.Show(projectId));
        }
    }
}
