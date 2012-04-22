using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Testing.DbIntegration.BaseFixtures;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.DbIntegration.Data.EF
{
    public class EFRepositoryTests : BaseIntegrationFixture
    {
        private IRepository<Gamer> repository;

        protected override void OnSetup()
        {
            this.repository = Container.Resolve<IRepository<Gamer>>();
        }

        [Test]
        public void Add_ShouldAddEntityToRepository()
        {
            var gamer = new Gamer();
            Assert.That(gamer.Id, Is.EqualTo(0));

            this.repository.AddPhysically(gamer);

            gamer.Id.Should().Be.GreaterThan(0);
            var gamerFromDb = this.repository.GetById(gamer.Id);
            gamerFromDb.Should().Not.Be.Null();
        }

        [Test]
        public void Delete_ShouldDeleteEntityFromDatabase()
        {
            var gamer = new Gamer();
            this.repository.AddPhysically(gamer);
            var gamerId = gamer.Id;

            this.repository.DeletePhysically(gamer);

            var gamerFromDb = this.repository.GetById(gamerId);
            gamerFromDb.Should().Be.Null();
        }

        [Test]
        public void GetById_ShouldReturnEntityById()
        {
            var gamer = new Gamer();
            this.repository.AddPhysically(gamer);
            var gamerId = gamer.Id;

            var gamerFromDb = this.repository.GetById(gamerId);

            gamerFromDb.Should().Be.EqualTo(gamer);
        }

        [Test]
        public void GetById_IfEntityWithPassedIdIsNotExistInDB_ShouldReturnNull()
        {
            var entityFromDb = this.repository.GetById(-1);

            entityFromDb.Should().Be.Null();
        }
    }
}
