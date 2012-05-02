using System;
using System.Collections.Generic;

namespace Gamification.Core.Entities
{
    [Serializable]
    public class GamerStatus : BaseEntity
    {
        public GamerStatus()
        {
            this.Gamers = new HashSet<Gamer>();
        }

        public virtual string StatusName { get; set; }

        public virtual ISet<Gamer> Gamers { get; protected set; }

        public virtual Project UserProject { get; set; }
    }
}