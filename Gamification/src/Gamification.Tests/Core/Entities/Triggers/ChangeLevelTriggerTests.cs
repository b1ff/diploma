using System.Linq;
using Gamification.Core.Entities;
using Gamification.Core.Entities.Triggers;
using Gamification.Testing.Utils.Extension;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Core.Entities.Triggers
{
    [TestFixture]
    public class ChangeLevelTriggerTests
    {
        private AutoChangeLevelTrigger autoChangeLevelTrigger;
        private Gamer gamer;

        [SetUp]
        public void SetUp()
        {
            this.autoChangeLevelTrigger = new AutoChangeLevelTrigger();
            this.gamer = new Gamer();
            gamer.Project = new Project();
            gamer.Project.Levels.Add(new Level { LevelNumber = 1, NeededPoints = 0 }.WithId());
            gamer.Project.Levels.Add(new Level { LevelNumber = 2, NeededPoints = 100 }.WithId(2));
            gamer.Project.Levels.Add(new Level { LevelNumber = 3, NeededPoints = 200 }.WithId(3));
        }

        [Test]
        public void CallOnGamer_WhenProjectIsNotUsePoints_ShouldNotDoAnything()
        {
            gamer.Project.UsePoints = false;
            gamer.Points = 1000;
            gamer.CurrentLevel = new Level().WithId(53);

            autoChangeLevelTrigger.CallOnGamer(gamer);

            gamer.Points.Should().Be.EqualTo(1000);
            gamer.CurrentLevel.Id.Should().Be(53);
        }

        [Test]
        public void CallOnGamer_WhenProjectIsUsePointsAndGamerPointsIsLessThanNeddedCurrentLevel_ShouldDecreaseLevel()
        {
            gamer.Project.UsePoints = true;
            gamer.Points = 90;
            gamer.CurrentLevel = gamer.Project.Levels.Last();

            autoChangeLevelTrigger.CallOnGamer(gamer);

            gamer.CurrentLevel.Should().Be(gamer.Project.Levels.First());
        }

        [Test]
        public void CallOnGamer_WhenProjectIsUsePointsAndGamerCanIncreaseLevel_ShouldIncreaseLevel()
        {
            gamer.Project.UsePoints = true;
            gamer.Points = 300;
            gamer.CurrentLevel = gamer.Project.Levels.First();

            autoChangeLevelTrigger.CallOnGamer(gamer);

            gamer.CurrentLevel.Should().Be(gamer.Project.Levels.Last());
        }
    }
}
