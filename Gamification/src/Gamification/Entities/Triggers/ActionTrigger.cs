using System.Collections.Generic;
using Gamification.Core.Extensions;

namespace Gamification.Core.Entities.Triggers
{
    public abstract class ActionTrigger : BaseEntity
    {
        protected ActionTrigger()
        {
            ChildTriggers = new List<ActionTrigger>();
        }

        public void CallOnGamer(Gamer gamer)
        {
            RaiseTriggerAction(gamer);
            if (ChildTriggers.IsEmpty()) 
                return;

            foreach (var childTrigger in ChildTriggers)
            {
                childTrigger.CallOnGamer(gamer);
            }
        }

        protected abstract void RaiseTriggerAction(Gamer gamer);

        public IList<ActionTrigger> ChildTriggers { get; set; }
    }
}