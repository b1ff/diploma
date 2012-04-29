using System.Linq;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities.Constraints;
using Gamification.Core.Enums;
using Gamification.Testing.Integration.BaseFixtures;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Integration.Entities
{
    public class QuantityBasedConstraintsTests : BaseIntegrationFixture
    {
        private IRepository<BaseNumericBasedConstraint> qtyBasedConstraintsRepository;

        protected override void OnSetup()
        {
            this.qtyBasedConstraintsRepository = this.Container.Resolve<IRepository<BaseNumericBasedConstraint>>();
        }

        [Test]
        public void WhenSaveDifferentTypesOfQtyBasedConstraints_ShouldGetFromDatabaseRightType()
        {
            var levelBasedConstraint = new LevelBasedConstraint();
            levelBasedConstraint.ValueToCompare = 10;
            levelBasedConstraint.Description = "needed level when user get 'God' status";
            this.qtyBasedConstraintsRepository.AddPhysically(levelBasedConstraint);

            var pointsBasedConstraint = new PointsBasedConstraint();
            pointsBasedConstraint.ValueToCompare = 2000;
            pointsBasedConstraint.Description = "needed points when user get 10 level";
            this.qtyBasedConstraintsRepository.AddPhysically(pointsBasedConstraint);

            Assert.That(levelBasedConstraint.Id, Is.GreaterThan(0));
            Assert.That(pointsBasedConstraint.Id, Is.GreaterThan(0));


            this.qtyBasedConstraintsRepository.ClearContext();
            var constraints = this.qtyBasedConstraintsRepository.ToList();


            var levelBsdFromDb = (LevelBasedConstraint)constraints.FirstOrDefault(x => x.Id == levelBasedConstraint.Id);
            var pointsBsdFromDb = (PointsBasedConstraint)constraints.FirstOrDefault(x => x.Id == pointsBasedConstraint.Id);
            levelBsdFromDb.Should().Be.EqualTo(levelBasedConstraint);
            pointsBsdFromDb.Should().Be.EqualTo(pointsBasedConstraint);
        }

        [Test]
        public void ShouldSaveEnumValuesToDb()
        {
            var levelBasedConstraint = new LevelBasedConstraint();
            levelBasedConstraint.BooleanOperation = BooleanOperations.Equals;
            this.qtyBasedConstraintsRepository.AddPhysically(levelBasedConstraint);

            var pointsBasedConstraint = new PointsBasedConstraint();
            pointsBasedConstraint.BooleanOperation = BooleanOperations.GreaterOrEquals;
            this.qtyBasedConstraintsRepository.AddPhysically(pointsBasedConstraint);

            this.qtyBasedConstraintsRepository.ClearContext();
            var constraints = this.qtyBasedConstraintsRepository.ToList();


            var levelBsdFromDb = (LevelBasedConstraint)constraints.FirstOrDefault(x => x.Id == levelBasedConstraint.Id);
            var pointsBsdFromDb = (PointsBasedConstraint)constraints.FirstOrDefault(x => x.Id == pointsBasedConstraint.Id);
            levelBsdFromDb.BooleanOperation.Should().Be.EqualTo(BooleanOperations.Equals);
            pointsBsdFromDb.BooleanOperation.Should().Be.EqualTo(BooleanOperations.GreaterOrEquals);
        }
    }
}
