using Prism.Ioc;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Views;
using System.Linq;
using Xamarin.Forms;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Bootstrapper
{
    internal static class PageRegister
    {
        internal static IContainerRegistry RegisterPages(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();

            /*typeof(PageRegister).Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType =>)*/

            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
            containerRegistry.RegisterForNavigation<HistoryPage, HistoryViewModel>();
            return containerRegistry;
        }
    }
}
