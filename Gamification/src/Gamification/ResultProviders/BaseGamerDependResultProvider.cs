using Gamification.Core.Entities;
using Gamification.Core.Operations;

namespace Gamification.Core.ResultProviders
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