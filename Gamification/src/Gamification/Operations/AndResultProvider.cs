namespace Gamification.Core.Operations
{
    public class AndResultProvider : BaseFromTwoResultsProviders
    {
        public AndResultProvider(IBooleanResultProvider firstResultProvider, IBooleanResultProvider secondResultProvider) : base(firstResultProvider, secondResultProvider)
        {
        }

        public override bool GetResult()
        {
            return this.first.GetResult() && this.second.GetResult();
        }
    }
}
