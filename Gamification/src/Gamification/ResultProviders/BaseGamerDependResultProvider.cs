using Gamification.Core.Entities;

namespace Gamification.Core.ResultProviders
{
    public abstract class BaseGamerDependResultProvider : BooleanResultProvider
    {
        protected BaseGamerDependResultProvider(Gamer gamer)
        {
            this.Gamer = gamer;
        }

        public Gamer Gamer { get; private set; }
    }
}