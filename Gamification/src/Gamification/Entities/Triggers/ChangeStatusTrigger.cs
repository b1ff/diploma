namespace Gamification.Core.Entities.Triggers
{
    public class ChangeStatusTrigger : ActionTrigger
    {
        public ChangeStatusTrigger(GamerStatus status)
        {
            this.StatusToChange = status;
        }

        public GamerStatus StatusToChange { get; set; }

        protected override void RaiseTriggerAction(Gamer gamer)
        {
            if (gamer.AcceptMultipleStatuses) 
                return;

            gamer.GamerStatuses.Clear();
            gamer.GamerStatuses.Add(this.StatusToChange);
        }
    }
}