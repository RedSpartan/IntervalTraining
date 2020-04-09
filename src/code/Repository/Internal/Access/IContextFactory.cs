namespace RedSpartan.IntervalTraining.Internal.Repository.Access
{
    public interface IContextFactory
    {
        DatabaseContext GetContext();
    }
}
