using RedSpartan.IntervalTraining.Common.Interfaces;

namespace RedSpartan.IntervalTraining.Internal.Repository.Access
{
    public class ContextFactory : IContextFactory
    {
        public string Path { get; }

        public ContextFactory(IDeviceSpecifics device)
        {
            Path = device.DbPath;
        }

        public DatabaseContext GetContext()
        {
            return new DatabaseContext(Path);
        }
    }
}
