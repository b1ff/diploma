using System;
using System.Linq;
using Gamification.Core.Entities;
using Gamification.Core.Entities.Constraints;
using Gamification.Core.Entities.Triggers;
using Gamification.Core.Enums;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Core.Entities
{
    [TestFixture]
    public class GameActionTests
    {
        private GameAction gameAction;

        [SetUp]
        public void SetUp()
        {
            this.gameAction = new GameAction();
        }

        [Test]
        public void PerformAction_WhenAnyOfQtyBasedConstraintsIsFail_ShouldNotPerfromAnyTrigger()
        {
            SetupActionWithFailedConstraintsAndTriggerWithException(null);
            var gamer = CreateGamerWithLevelAndPoints(1, 1000);
            
            this.gameAction.PerformAction(gamer);

            Assert.Pass();
        }

        [Test]
        public void PerformAction_WhenAnyOfQtyBasedConstraintsIsFail_ShouldReturnFailErrors()
        {
            const string levelIsToSmall = "Level is too small";
            SetupActionWithFailedConstraintsAndTriggerWithException(levelIsToSmall);
            var gamer = CreateGamerWithLevelAndPoints(1, 1000);

            var errors = this.gameAction.PerformAction(gamer);

            errors.First().Message.Should().Be.EqualTo(levelIsToSmall);
        }

        private void SetupActionWithFailedConstraintsAndTriggerWithException(string failMessage)
        {
            var levelConstraint = new LevelBasedConstraint { ValueToCompare = 2 };
            levelConstraint.BooleanOperation = BooleanOperations.GreaterOrEquals;
            levelConstraint.Description = failMessage;
            var pointsConstraint = new PointsBasedConstraint { ValueToCompare = 102 };
            pointsConstraint.BooleanOperation = BooleanOperations.GreaterThan;
            this.gameAction.QtyBasedConstraints.Add(levelConstraint);
            this.gameAction.QtyBasedConstraints.Add(pointsConstraint);
            var trigger = new Mock<ActionTrigger>();
            trigger.Protected().Setup("RaiseTriggerAction", ItExpr.IsAny<Gamer>())
                .Callback((Gamer g) => { throw new InvalidOperationException("Trigger is fired"); });
            this.gameAction.TriggersToCall.Add(trigger.Object);
        }

        private static Gamer CreateGamerWithLevelAndPoints(int level, int points)
        {
            var gamer = new Gamer();
            gamer.CurrentLevel = new Level { LevelNumber = level };
            gamer.Points = points;
            return gamer;
        }
    }
}
