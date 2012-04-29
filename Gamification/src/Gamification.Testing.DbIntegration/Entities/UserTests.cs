using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Testing.Integration.BaseFixtures;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Integration.Entities
{
    [TestFixture]
    public class UserTests : BaseIntegrationFixture
    {
        private IUsersRepository usersRepo;
        private IRepository<Project> projectsRepo;

        protected override void OnSetup()
        {
            this.usersRepo = Container.Resolve<IUsersRepository>();
            this.projectsRepo = Container.Resolve<IRepository<Project>>();
        }
    
        [Test]
        public void WhenSaveProjectWithUser_UserProjectsCollectionShouldContainsThisProject()
        {
            var user = new User();
            this.usersRepo.AddPhysically(user);
            var userId = user.Id;
            var project = new Project();
            project.User = user;
            this.projectsRepo.AddPhysically(project);
            var projectId = project.Id;
            this.projectsRepo.ClearContext();

            project = this.projectsRepo.GetById(projectId);
            user = this.usersRepo.GetById(userId);

            user.Projects.Should().Contain(project);
        }
    }
}