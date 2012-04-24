namespace Gamification.Core.Entities.Triggers
{
    public abstract class ActionTrigger : BaseEntity
    {
        public abstract void CallOnGamer(Gamer gamer);
    }
}