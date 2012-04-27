namespace Gamification.Core.ResultProviders
{
    public abstract class BaseFromTwoResultsProviders : BooleanResultProvider
    {
        protected readonly BooleanResultProvider first;
        protected readonly BooleanResultProvider second;

        protected BaseFromTwoResultsProviders(BooleanResultProvider first, BooleanResultProvider second)
        {
            this.first = first;
            this.second = second;
        }
    }
}