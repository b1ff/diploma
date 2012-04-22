using System.Collections.Generic;

namespace Gamification.Core.Entities
{
    public class GamerStatus : BaseEntity
    {
        public GamerStatus()
        {
            this.Gamers = new HashSet<Gamer>();
            this.UserProjects = new HashSet<UserProject>();
        }

        public virtual string StatusName { get; set; }

        public virtual ISet<Gamer> Gamers { get; protected set; }

        public virtual ISet<UserProject> UserProjects { get; protected set; }
    }
}