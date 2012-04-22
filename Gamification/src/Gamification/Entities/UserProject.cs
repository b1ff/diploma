using System;
using System.Collections.Generic;

namespace Gamification.Core.Entities
{
    public class UserProject : BaseEntity
    {
        public UserProject()
        {
            this.Gamers = new HashSet<Gamer>();
        }

        public virtual Guid UserKey { get; set; }

        public virtual Guid GamerKey { get; set; }

        public virtual ISet<Gamer> Gamers { get; protected set; }
    }
}