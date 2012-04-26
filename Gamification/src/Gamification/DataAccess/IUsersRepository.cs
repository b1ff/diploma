using Gamification.Core.Entities;

namespace Gamification.Core.DataAccess
{
    public interface IUsersRepository : IRepository<User>
    {
        User GetCurrentUser();
    }
}
