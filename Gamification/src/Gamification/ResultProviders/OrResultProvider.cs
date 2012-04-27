namespace Gamification.Core.ResultProviders
{
    public class OrResultProvider : BaseFromTwoResultsProviders
    {
        public OrResultProvider(BooleanResultProvider first, BooleanResultProvider second) : base(first, second)
        {
        }

        public override bool GetResult()
        {
            return first.GetResult() || second.GetResult();
        }
    }
}