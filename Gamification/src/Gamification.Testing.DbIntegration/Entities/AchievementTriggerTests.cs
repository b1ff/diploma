using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Core.Entities.Triggers;
using Gamification.Testing.Integration.BaseFixtures;
using Gamification.Testing.Integration.Utils;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Integration.Entities
{
    [TestFixture]
    public class AchievementTriggerTests : BaseIntegrationFixture
    {
        [Test]
        public void GetAchievementTriggerFromDb_ShouldAlwaysIncludeAchievement()
        {
            var repository = this.Container.Resolve<IRepository<AchievementsTrigger>>();
            var achievementRepository = this.Container.Resolve<IRepository<Achievement>>();
            var achievement = new Achievement { Name = "achievement" };
            achievementRepository.AddPhysically(achievement);
            var achievementTrigger = new AchievementsTrigger();
            achievementTrigger.Achievement = achievement;
            repository.AddPhysically(achievementTrigger);
            repository.ClearContext();
            var container = IntegrationTestsComponentRegistration.CreateTestsWindsorContainer();
            var newRepo = container.Resolve<IRepository<AchievementsTrigger>>();

            var triggerFromDb = newRepo.GetById(achievementTrigger.Id);

            triggerFromDb.Achievement.Should().Not.Be.Null();
            triggerFromDb.Achievement.Name.Should().Be.EqualTo("achievement");
        }
    }
}
