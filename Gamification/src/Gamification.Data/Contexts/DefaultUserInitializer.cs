using System.Data.Entity;
using Gamification.Core.Entities;
using Gamification.Web.Utils;

namespace Gamification.Data.EF.Contexts
{
    public class DefaultUserInitializer<TContext> : DropCreateDatabaseIfModelChanges<TContext> 
        where TContext : DbContext
    {
        protected override void Seed(TContext context)
        {
            base.Seed(context);
            var user = new User();
            user.Username = "user";
            user.PasswordHash = "password".GetHash();

            context.Set<User>().Add(user);
            context.SaveChanges();
        }
    }
}
