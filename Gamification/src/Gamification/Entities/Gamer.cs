using System.Collections.Generic;

namespace Gamification.Core.Entities
{
    public class Gamer : BaseEntity
    {
        public Gamer()
        {
            this.Achievements = new List<Achievement>();
            this.GamerStatuses = new HashSet<GamerStatus>();
        }

        public Project Project { get; set; }

        public long Points { get; set;  }

        public ISet<GamerStatus> GamerStatuses { get; set; }

        public Level CurrentLevel { get; set; }

        public IList<Achievement> Achievements { get; set; }
    }
}