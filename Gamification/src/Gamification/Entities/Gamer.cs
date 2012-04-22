using System.Collections.Generic;

namespace Gamification.Core.Entities
{
    public class Gamer : BaseEntity
    {
        public Gamer()
        {
            this.GamerStatuses = new HashSet<GamerStatus>();
            this.Achievements = new List<Achievement>();
        }

        public virtual Project Project { get; set; }

        public virtual long Points { get; set;  }

        public virtual ISet<GamerStatus> GamerStatuses { get; protected set; }

        public Level CurrentLevel { get; set; }

        public IList<Achievement> Achievements { get; protected set; }
    }
}