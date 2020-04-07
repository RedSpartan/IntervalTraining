using Prism.Navigation;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "Home";
        }
    }
}
