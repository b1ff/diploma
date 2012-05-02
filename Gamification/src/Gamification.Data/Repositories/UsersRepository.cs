using System.Collections.Generic;
using System.Web;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities;
using Gamification.Data.EF.Contexts;

namespace Gamification.Data.EF.Repositories
{
    public class UsersRepository : EfRepository<User>, IUsersRepository
    {
        private readonly HttpContextBase httpContext;
        private readonly IRepository<User> userRepository;

        public UsersRepository(HttpContextBase httpContext, IRepository<User> userRepository, EfDbContext dbContext) 
            : base(dbContext)
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
