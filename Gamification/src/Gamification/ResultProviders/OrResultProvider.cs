using Gamification.Core.Operations;

namespace Gamification.Core.ResultProviders
{
    public class OrResultProvider : BaseFromTwoResultsProviders
    {
        public OrResultProvider(IBooleanResultProvider first, IBooleanResultProvider second) : base(first, second)
        {
        }

        public override bool GetResult()
        {
            return first.GetResult() || second.GetResult();
        }
    }
}