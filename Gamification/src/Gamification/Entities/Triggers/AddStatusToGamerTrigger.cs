namespace Gamification.Core.Entities.Triggers
{
    public class AddStatusToGamerTrigger : ActionTrigger
    {
        public AddStatusToGamerTrigger(GamerStatus status)
        {
            StatusToAdd = status;
        }

        public GamerStatus StatusToAdd { get; set; }

        protected override void RaiseTriggerAction(Gamer gamer)
        {
            if (gamer.AcceptMultipleStatuses && !gamer.GamerStatuses.Contains(StatusToAdd))
            {
                gamer.GamerStatuses.Add(StatusToAdd);
            }
        }
    }
}