namespace Gamification.Core.Entities
{
    public abstract class ActionTrigger : BaseEntity
    {
        public abstract void TriggerOnGamer(Gamer gamer);
    }
}