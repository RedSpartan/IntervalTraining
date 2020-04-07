namespace RedSpartan.IntervalTraining.Repository.Access
{
    public interface IContextFactory
    {
        DatabaseContext GetContext();
    }
}
