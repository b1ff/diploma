using Gamification.Core.Enums;

namespace Gamification.Core.Entities.Triggers
{
    public class ChangePointsTrigger : ActionTrigger
    {
        public ChangePointsTrigger()
        {
            ChildTriggers.Add(new AutoChangeLevelTrigger());
        }

        public PointsOperation PointsOperation
        {
            get { return (PointsOperation)PointsOperationId; }
            set { PointsOperationId = (byte)value; }
        }

        public int Points { get; set; }

        public byte PointsOperationId { get; set; }

        protected override void RaiseTriggerAction(Gamer gamer)
        {
            if (PointsOperation == PointsOperation.Decrease)
                gamer.Points -= this.Points;
            else
                gamer.Points += this.Points;
        }
    }
}