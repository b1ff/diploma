using System.Collections.Generic;
using Gamification.Core.Domain;
using Gamification.Core.Entities.Constraints;
using Gamification.Core.Extensions;

namespace Gamification.Core.Entities
{
    public class GameAction : BaseEntity
    {
        public GameAction()
        {
            TriggersToCall = new List<ActionTrigger>();
            QtyBasedConstraints = new HashSet<BaseQuantityBasedConstraint>();
            StringCollectionConstraints = new HashSet<BaseStringCollectionConstraint>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public IList<ActionTrigger> TriggersToCall { get; protected set; }

        public bool IsMultiple { get; set; }

        public ISet<BaseQuantityBasedConstraint> QtyBasedConstraints { get; protected set; }

        public ISet<BaseStringCollectionConstraint> StringCollectionConstraints { get; protected set; } 

        public IEnumerable<ActionPerformError> PerformAction(Gamer gamer)
        {
            var errors = new List<ActionPerformError>();
            foreach (var constraint in QtyBasedConstraints)
            {
                var result = constraint.GetResult(gamer);
                if (!result)
                {
                    errors.Add(new ActionPerformError(constraint.Description));
                }
            }

            if (errors.IsEmpty())
            {
                foreach (var trigger in TriggersToCall)
                {
                    trigger.CallOnGamer(gamer);
                }
            }

            return errors;
        }
    }
}