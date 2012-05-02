using Gamification.Core.Entities;
using Gamification.Core.Entities.Constraints;
using Gamification.Core.Entities.Triggers;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Core.Entities
{
    [TestFixture]
    public class GamerTests
    {
        [Test]
        public void Clone_ShoulCopyAllGamerProperties()
        {
            var gamer = new Gamer();
            gamer.Project = new Project();
            gamer.Project.GameActions.Add(new GameAction());
            gamer.Project.NumericConstraints.Add(new PointsBasedConstraint());
            gamer.Project.Triggers.Add(new AchievementsTrigger());
            gamer.Project.CollectionConstraints.Add(new AchievementsConstraint());
            gamer.Achievements.Add(new Achievement());
            gamer.GamerStatuses.Add(new GamerStatus());
            gamer.CurrentLevel = new Level { LevelNumber = 10 };

            var clonedGamer = gamer.Clone();

            clonedGamer.Should().Not.Be.Null();
            Assert.That(ReferenceEquals(gamer, clonedGamer), Is.False);
            clonedGamer.Project.Should().Not.Be.Null();
            clonedGamer.Achievements.Should().Have.Count.EqualTo(1);
            clonedGamer.GamerStatuses.Should().Have.Count.EqualTo(1);
            clonedGamer.CurrentLevel.LevelNumber.Should().Be.EqualTo(10);
        }
    }
}
