using System.Collections.Generic;

namespace Gamification.Core.Entities
{
    public class GameAction : BaseEntity
    {
        public GameAction()
        {
            TriggersToCall = new List<ActionTrigger>();
        }

        public string Description { get; set; }

        public IList<ActionTrigger> TriggersToCall { get; protected set; }

        public bool IsMultiple { get; set; }
    }
}