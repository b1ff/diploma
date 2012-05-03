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

        public string StatusName { get; set; }

        public virtual ISet<Gamer> Gamers { get; protected set; }

        public Project UserProject { get; set; }
    }
}