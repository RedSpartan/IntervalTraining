using Autofac;
using RedSpartan.IntervalTraining.Bootstrap;
using RedSpartan.IntervalTraining.ViewModels;

namespace RedSpartan.IntervalTraining.Services
{
    public class ViewModelLocator
    {
        public AboutViewModel About => AppContainer.Container.Resolve<AboutViewModel>();
        public ItemDetailViewModel ItemDetail => AppContainer.Container.Resolve<ItemDetailViewModel>();
        public ItemsViewModel Items => AppContainer.Container.Resolve<ItemsViewModel>();
    }
}
