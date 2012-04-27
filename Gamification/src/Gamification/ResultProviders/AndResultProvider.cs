namespace Gamification.Core.ResultProviders
{
    public class AndResultProvider : BaseFromTwoResultsProviders
    {
        public AndResultProvider(BooleanResultProvider firstResultProvider, BooleanResultProvider secondResultProvider) : base(firstResultProvider, secondResultProvider)
        {
        }

        public override bool GetResult()
        {
            return this.first.GetResult() && this.second.GetResult();
        }
    }
}
