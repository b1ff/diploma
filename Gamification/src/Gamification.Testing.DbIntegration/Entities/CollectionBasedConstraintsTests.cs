using System.Linq;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities.Constraints;
using Gamification.Core.Enums;
using Gamification.Testing.Integration.BaseFixtures;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Integration.Entities
{
    public class CollectionBasedConstraintsTests : BaseIntegrationFixture
    {
        private IRepository<BaseStringCollectionConstraint> collectionConstraintsRepository;

        protected override void OnSetup()
        {
            this.collectionConstraintsRepository = this.Container.Resolve<IRepository<BaseStringCollectionConstraint>>();
        }

        [Test]
        public void WhenSaveDifferentTypesOfCollectionBasedConstraints_ShouldGetFromDatabaseRightType()
        {
            var achievementsConstraint = new AchievementsConstraint();
            achievementsConstraint.ValueToCompare = "Man";
            achievementsConstraint.Description = "needed achievement for do things that people do";
            this.collectionConstraintsRepository.AddPhysically(achievementsConstraint);

            var statusConstraint = new StatusConstraint();
            statusConstraint.CollectionEqualityOperation = CollectionEqualityOperations.Have;
            statusConstraint.ValueToCompare = "God";
            statusConstraint.Description = "needed status for creation of Earth";
            this.collectionConstraintsRepository.AddPhysically(statusConstraint);

            Assert.That(achievementsConstraint.Id, Is.GreaterThan(0));
            Assert.That(statusConstraint.Id, Is.GreaterThan(0));

            this.collectionConstraintsRepository.ClearContext();
            var constraints = this.collectionConstraintsRepository.ToList();


            var levelBsdFromDb = (AchievementsConstraint)constraints.FirstOrDefault(x => x.Id == achievementsConstraint.Id);
            var pointsBsdFromDb = (StatusConstraint)constraints.FirstOrDefault(x => x.Id == statusConstraint.Id);
            levelBsdFromDb.Should().Be.EqualTo(achievementsConstraint);
            pointsBsdFromDb.Should().Be.EqualTo(statusConstraint);
        }

        [Test]
        public void ShouldSaveEnumValuesToDb()
        {
            var achievementsConstraint = new AchievementsConstraint();
            achievementsConstraint.CollectionEqualityOperation = CollectionEqualityOperations.Have;
            this.collectionConstraintsRepository.AddPhysically(achievementsConstraint);

            var statusConstraint = new StatusConstraint();
            statusConstraint.CollectionEqualityOperation = CollectionEqualityOperations.NotHave;
            this.collectionConstraintsRepository.AddPhysically(statusConstraint);

            this.collectionConstraintsRepository.ClearContext();
            var constraints = this.collectionConstraintsRepository.ToList();


            var levelBsdFromDb = (AchievementsConstraint)constraints.FirstOrDefault(x => x.Id == achievementsConstraint.Id);
            var pointsBsdFromDb = (StatusConstraint)constraints.FirstOrDefault(x => x.Id == statusConstraint.Id);
            levelBsdFromDb.CollectionEqualityOperation.Should().Be.EqualTo(CollectionEqualityOperations.Have);
            pointsBsdFromDb.CollectionEqualityOperation.Should().Be.EqualTo(CollectionEqualityOperations.NotHave);
        }
    }
}
