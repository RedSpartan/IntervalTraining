using Prism.Navigation;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }
    }
}
