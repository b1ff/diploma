using System.Collections.Generic;
using Gamification.Core.Extensions;

namespace Gamification.Core.Entities.Triggers
{
    public abstract class ActionTrigger : BaseEntity
    {
        protected ActionTrigger()
        {
            ChildTriggers = new List<ActionTrigger>();
            this.GameActions = new List<GameAction>();
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

        public IList<GameAction> GameActions { get; set; }

        public Project Project { get; set; }

        public string Title { get; set; }

        public void SetProject(Project project)
        {
            this.Project = project;
        }

        protected virtual void OnSetProject(Project project)
        {
            foreach(var trigger in this.ChildTriggers)
            {
                trigger.SetProject(project);
            }
        }
    }
}