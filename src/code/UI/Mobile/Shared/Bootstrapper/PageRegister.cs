using Prism.Ioc;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Views;
using Xamarin.Forms;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Bootstrapper
{
    internal static class PageRegister
    {
        internal static IContainerRegistry RegisterPages(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
            containerRegistry.RegisterForNavigation<HistoryPage, HistoryViewModel>();
            return containerRegistry;
        }
    }
}
