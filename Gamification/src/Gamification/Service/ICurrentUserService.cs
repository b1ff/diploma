using Gamification.Core.Entities;

namespace Gamification.Core.Service
{
    public interface ICurrentUserService
    {
        User GetCurrentUser();
    }
}
