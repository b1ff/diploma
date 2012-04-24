using System.ComponentModel.DataAnnotations;
using Gamification.Core.Enums;

namespace Gamification.Core.Entities.Triggers
{
    public class PointsTrigger : ActionTrigger
    {
        public PointsOperation PointsOperation
        {
            get { return (PointsOperation)PointsOperationId; }
            set { PointsOperationId = (byte)value; }
        }

        public int Points { get; set; }

        [Column("PointsOperation")]
        public byte PointsOperationId { get; set; }

        public override void CallOnGamer(Gamer gamer)
        {
            if (PointsOperation == PointsOperation.Decrease)
                gamer.Points -= this.Points;
            else
                gamer.Points += this.Points;
        }
    }
}