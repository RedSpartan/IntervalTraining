using Autofac;
using RedSpartan.IntervalTraining.ViewModels;

namespace RedSpartan.IntervalTraining.Services
{
    public class ViewModelLocator
    {
        public AboutViewModel About => AppContainer.Container.Resolve<AboutViewModel>();
    }
}
