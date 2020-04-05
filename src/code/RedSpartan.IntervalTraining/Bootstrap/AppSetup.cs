using Autofac;
using RedSpartan.IntervalTraining.ViewModels;

namespace RedSpartan.IntervalTraining.Bootstrap
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);
            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder cb)
        {
            cb.RegisterType<AboutViewModel>().SingleInstance();
        }
    }
}
