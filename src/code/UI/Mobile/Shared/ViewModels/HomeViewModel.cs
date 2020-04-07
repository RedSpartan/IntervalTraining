using Prism.Commands;
using Prism.Navigation;
using RedSpartan.IntervalTraining.Repository.DTOs;
using RedSpartan.IntervalTraining.Repository.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class HomeViewModel : ViewModelBase, IInitializeAsync
    {
        #region Services
        public IDataService<IntervalTemplateDto> IntervalService { get; }
        #endregion Services

        #region Commands
        public ICommand AddNewIntervalTemplateCommand { get; }
        #endregion Commands

        #region Collections
        public ObservableCollection<IntervalTemplateDto> IntervalTemplates { get; } = new ObservableCollection<IntervalTemplateDto>();
        #endregion Collections

        public HomeViewModel(INavigationService navigationService,
            IDataService<IntervalTemplateDto> intervalService) 
            : base(navigationService)
        {
            Title = "Home";
            IntervalService = intervalService ?? throw new ArgumentNullException(nameof(intervalService));
            AddNewIntervalTemplateCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("NewIntervalTemplatePage", useModalNavigation: true));
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            foreach (var template in await IntervalService.GetItemsAsync())
            {
                IntervalTemplates.Add(template);
            }
        }
    }
}
