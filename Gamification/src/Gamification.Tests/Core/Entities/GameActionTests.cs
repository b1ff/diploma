using Gamification.Core.Entities;
using Gamification.Core.Entities.Constraints;
using NUnit.Framework;

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
        public void PerformAction_WhenAnyOfConstraintIsFail_ShouldNotPerfromAnyTrigger()
        {
            //this.gameAction.Constraints.Add(new BaseBooleanConstraint())
        }
    }
}
