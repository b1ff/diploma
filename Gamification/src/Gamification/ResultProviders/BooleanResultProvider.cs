namespace Gamification.Core.ResultProviders
{
    public abstract class BooleanResultProvider
    {
        public abstract bool GetResult();

        public static BooleanResultProvider operator & (BooleanResultProvider left, BooleanResultProvider right)
        {
            return new AndResultProvider(left, right);
        }

        public static BooleanResultProvider operator | (BooleanResultProvider left, BooleanResultProvider right)
        {
            return new OrResultProvider(left, right);
        }
    }
}
