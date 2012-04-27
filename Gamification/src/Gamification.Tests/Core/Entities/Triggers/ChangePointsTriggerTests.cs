using Gamification.Core.Entities;
using Gamification.Core.Entities.Triggers;
using Gamification.Core.Enums;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Core.Entities.Triggers
{
    [TestFixture]
    public class ChangePointsTriggerTests
    {
        private ChangePointsTrigger pointsTrigger;

        [SetUp]
        public void SetUp()
        {
            this.pointsTrigger = new ChangePointsTrigger();
            this.pointsTrigger.ChildTriggers.Clear();
        }

        [Test]
        public void CallOnGamer_WhenPointsOperationIsDecrease_ShouldDecreaseGamerPointsOnPointsValue()
        {
            pointsTrigger.PointsOperation = PointsOperation.Decrease;
            pointsTrigger.Points = 10;
            var gamer = new Gamer();
            gamer.Points = 20;

            pointsTrigger.CallOnGamer(gamer);

            gamer.Points.Should().Be.EqualTo(10);
        }

        [Test]
        public void CallOnGamer_WhenPointsOperationIsIncrease_ShouldIncreaseGamerPointsOnPointsValue()
        {
            pointsTrigger.Points = 10;
            pointsTrigger.PointsOperation = PointsOperation.Increase;
            var gamer = new Gamer();
            gamer.Points = 20;

            pointsTrigger.CallOnGamer(gamer);

            gamer.Points.Should().Be.EqualTo(30);
        }
    }
}
