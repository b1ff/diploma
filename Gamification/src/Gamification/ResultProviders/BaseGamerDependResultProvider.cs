using Gamification.Core.Entities;

namespace Gamification.Core.Operations
{
    public abstract class BaseGamerDependResultProvider : IBooleanResultProvider
    {
        protected BaseGamerDependResultProvider(Gamer gamer)
        {
            this.Gamer = gamer;
        }

        public Gamer Gamer { get; private set; }

        public abstract bool GetResult();
    }
}