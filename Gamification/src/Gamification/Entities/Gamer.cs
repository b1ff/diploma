using System.Collections.Generic;

namespace Gamification.Core.Entities
{
    public class Gamer : BaseEntity
    {
        public virtual UserProject UserProject { get; set; }

        public virtual long Points { get; set;  }

        public virtual ISet<GamerStatus> GamerStatuses { get; protected set; }
    }
}