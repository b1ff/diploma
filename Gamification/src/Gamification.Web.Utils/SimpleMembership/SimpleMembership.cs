using System.Web.Security;
using Gamification.Core;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Core.Specifications;

namespace Gamification.Web.Utils.SimpleMembership
{
    public class SimpleMembership : ISimpleMembership
    {
        private readonly IUsersRepository usersRepository;

        public SimpleMembership(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public bool ValidateUser(string username, string password)
        {
            var user = usersRepository.FirstBySpec(new UserByNameSpec(username));
            if (user == null)
                return false;

            return user.PasswordHash == password.GetHash();
        }

        public MembershipCreateStatus CreateUser(string username, string password, string email)
        {
            Guard.StringArgumentIsNullOrBlank(username, "username");
            Guard.StringArgumentIsNullOrBlank(password, "password");
            Guard.StringArgumentIsNullOrBlank(email, "email");

            var user = usersRepository.FirstBySpec(new UserByNameSpec(username));
            if (user != null)
                return MembershipCreateStatus.DuplicateUserName;

            user = usersRepository.FirstBySpec(new UserByEmailSpec(email));
            if (user != null)
                return MembershipCreateStatus.DuplicateEmail;

            var createdUser = new User
                                  {
                                      Username = username,
                                      PasswordHash = password.GetHash(),
                                      Email = email
                                  };
            this.usersRepository.AddPhysically(createdUser);
            return MembershipCreateStatus.Success;
        }

        public bool ChangeCurrentUserPassword(string oldPassword, string newPassword)
        {
            Guard.StringArgumentIsNullOrBlank(newPassword, "newPassword");

            var user = usersRepository.GetCurrentUser();
            if (user == null || user.PasswordHash != oldPassword.GetHash())
                return false;

            user.PasswordHash = newPassword.GetHash();
            try
            {
                this.usersRepository.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void LogInUser(string username, bool isPersistence)
        {
            FormsAuthentication.SetAuthCookie(username, isPersistence);
        }

        public void LogOutCurrentUser()
        {
            FormsAuthentication.SignOut();
        }
    }
}