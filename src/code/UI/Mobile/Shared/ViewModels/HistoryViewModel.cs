using Prism.Navigation;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class HistoryViewModel : ViewModelBase
    {
        public HistoryViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "History";
        }
    }
}
