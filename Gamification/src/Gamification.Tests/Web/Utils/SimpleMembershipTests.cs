using System;
using System.Web.Security;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Core.Specifications;
using Gamification.Testing.Utils;
using Gamification.Web.Utils;
using Gamification.Web.Utils.SimpleMembership;
using Moq;
using NUnit.Framework;
using SharpTestsEx;

namespace Gamification.Testing.Unit.Web.Utils
{
    [TestFixture]
    public class SimpleMembershipTests : BaseAutoMoqFixtureWithSubject<SimpleMembership>
    {
        [Test]
        public void ValidateUser_WhenUsernameAndPasswordHashExistInDatabase_ShouldReturnTrue()
        {
            const string username = "username";
            const string password = "password";
            Setup<IUsersRepository, User>(x => x.FirstBySpec(It.IsAny<UserByNameSpec>()))
                .Returns(new User { Username = username, PasswordHash = password.GetHash() });

            var result = Subject.ValidateUser(username, password);

            result.Should().Be.True();
        }

        [Test]
        public void ValidateUser_WhenUserWasNotFoundedByUsername_ShouldReturnFalse()
        {
            const string username = "username";
            const string password = "password";
            Setup<IUsersRepository, User>(x => x.FirstBySpec(It.IsAny<UserByNameSpec>()))
                .Returns((User)null);

            var result = Subject.ValidateUser(username, password);

            result.Should().Be.False();
        }

        [Test]
        public void ValidateUser_WhenPasswordHashNotEqualToExistingInDatabase_ShouldReturnFalse()
        {
            const string username = "username";
            const string password = "password";
            Setup<IUsersRepository, User>(x => x.FirstBySpec(It.IsAny<UserByNameSpec>()))
                .Returns(new User { Username = username, PasswordHash = "1".GetHash() });

            var result = Subject.ValidateUser(username, password);

            result.Should().Be.False();
        }

        [Test]
        public void CreateUser_WhenAlredyExistUserWithSameName_ShouldReturnDublicateUsernameResult()
        {
            Setup<IUsersRepository, User>(x => x.FirstBySpec(It.IsAny<UserByNameSpec>()))
                .Returns(new User());

            var result = Subject.CreateUser("username", "pass", "email");

            result.Should().Be.EqualTo(MembershipCreateStatus.DuplicateUserName);
        }

        [Test]
        public void CreateUser_WhenAlreadyExistUserWithSameEmail_ShouldReturnDublicateEmailResult()
        {
            Setup<IUsersRepository, User>(x => x.FirstBySpec(It.IsAny<UserByEmailSpec>()))
                .Returns(new User());

            var result = Subject.CreateUser("username", "pass", "email");

            result.Should().Be.EqualTo(MembershipCreateStatus.DuplicateEmail);
        }

        [Test]
        public void CreateUser_WhenResultIsSuccess_ShouldCreateUserAndSaveToDatabase()
        {
            const string username = "username";
            const string password = "password";
            const string email = "email";
            var expectedPassHash = password.GetHash();

            Subject.CreateUser(username, password, email);

            Verify<IUsersRepository>(x => x.AddPhysically(
                It.Is<User>(
                u => u.Username == username && u.PasswordHash == expectedPassHash && u.Email == email)));
        }

        [Test]
        public void CreateUser_WhenUsernameIsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Subject.CreateUser(null, "pass", "email"));
        }

        [Test]
        public void CreateUser_WhenPassIsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Subject.CreateUser("username", null, "email"));
        }

        [Test]
        public void CreateUser_WhenEmailIsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Subject.CreateUser("username", "pass", null));
        }
    }
}
