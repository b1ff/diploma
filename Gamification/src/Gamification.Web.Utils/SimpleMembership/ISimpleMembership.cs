using System.Web.Security;

namespace Gamification.Web.Utils.SimpleMembership
{
    public interface ISimpleMembership
    {
        bool ValidateUser(string username, string password);

        void LogInUser(string username, bool isPersistence);

        void LogOutCurrentUser();

        MembershipCreateStatus CreateUser(string username, string password, string email);

        bool ChangeCurrentUserPassword(string oldPassword, string newPassword);
    }
}
