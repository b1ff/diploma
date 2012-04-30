using Gamification.Core.Enums;

namespace Gamification.Core.Entities.Triggers
{
    public class AddOrRemoveStatusTrigger : ActionTrigger
    {
        public AddOrRemoveStatusTrigger()
        {
        }

        public AddOrRemoveStatusTrigger(GamerStatus status)
        {
            Status = status;
        }

        public GamerStatus Status { get; set; }

        public AssignUnassign StatusAction
        {
            get { return (AssignUnassign)StatusActionId; }
            set { StatusActionId = (byte)value; }
        }

        public byte StatusActionId { get; set; }

        protected override void RaiseTriggerAction(Gamer gamer)
        {
            if (gamer.Project.AcceptMultipleStatuses)
            {
                RaiseForMultipleStatuses(gamer);
            }
            else
            {
                RaiceForSingleStatus(gamer);
            }
        }

        private void RaiceForSingleStatus(Gamer gamer)
        {
            gamer.GamerStatuses.Clear();
            if (StatusAction == AssignUnassign.Assign)
            {
                gamer.GamerStatuses.Add(this.Status);
            }
        }

        private void RaiseForMultipleStatuses(Gamer gamer)
        {
            if (StatusAction == AssignUnassign.Assign)
            {
                if (!gamer.GamerStatuses.Contains(Status))
                    gamer.GamerStatuses.Add(Status);
            }
            else
            {
                if (gamer.GamerStatuses.Contains(Status))
                    gamer.GamerStatuses.Remove(Status);
            }
        }
    }
}