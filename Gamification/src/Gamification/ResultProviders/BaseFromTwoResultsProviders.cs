using Gamification.Core.Operations;

namespace Gamification.Core.ResultProviders
{
    public abstract class BaseFromTwoResultsProviders : IBooleanResultProvider
    {
        protected readonly IBooleanResultProvider first;
        protected readonly IBooleanResultProvider second;

        protected BaseFromTwoResultsProviders(IBooleanResultProvider first, IBooleanResultProvider second)
        {
            this.first = first;
            this.second = second;
        }

        public abstract bool GetResult();
    }
}