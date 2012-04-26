namespace Gamification.Testing.Utils
{
    public abstract class BaseAutoMoqFixtureWithSubject<TSubject> : BaseAutoMoqFixture
        where TSubject : class
    {
        protected TSubject Subject { get; set; }

        protected override void OnSetUp()
        {
            Subject = CreateInctanceWithAutoMockedDependencies<TSubject>();
            OnSetup();
        }

        protected virtual void OnSetup() { }
    }
}