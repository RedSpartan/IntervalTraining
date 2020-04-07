using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {

        #region Commands
        public ICommand AddNewIntervalTemplateCommand { get; }
        #endregion Commands

        public HomeViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "Home";
            AddNewIntervalTemplateCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("NewIntervalTemplatePage", useModalNavigation: true));
        }
    }
}
