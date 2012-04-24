using System.Web;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Core.Service;

namespace Gamification.Data.EF.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContextBase httpContext;
        private readonly IRepository<User> userRepository;

        public CurrentUserService(HttpContextBase httpContext, IRepository<User> userRepository)
        {
            this.httpContext = httpContext;
            this.userRepository = userRepository;
        }

        public User GetCurrentUser()
        {
            if (httpContext != null && httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
            {
                var user = this.userRepository.FirstOrDefault(x => x.Username == httpContext.User.Identity.Name);
                return user;
            }

            return null;
        }
    }
}
