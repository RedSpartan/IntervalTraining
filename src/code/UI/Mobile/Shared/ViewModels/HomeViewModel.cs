using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using RedSpartan.IntervalTraining.Repository.DTOs;
using RedSpartan.IntervalTraining.Repository.Services;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Events;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class HomeViewModel : ViewModelBase, IInitializeAsync
    {
        #region Collections
        public ObservableCollection<IntervalTemplate> IntervalTemplates { get; } = new ObservableCollection<IntervalTemplate>();
        #endregion Collections

        #region Services
        private IDataService<IntervalTemplateDto> IntervalService { get; }
        private IEventAggregator EventAggregator { get; }
        private IMapper Mapper { get; }
        #endregion Services

        #region Commands
        public ICommand AddEditIntervalTemplateCommand { get; }
        public ICommand OpenTimerCommand { get; }
        #endregion Commands

        #region Constructor
        public HomeViewModel(INavigationService navigationService,
            IDataService<IntervalTemplateDto> intervalService,
            IEventAggregator eventAggregator,
            IMapper mapper)
            : base(navigationService)
        {
            Title = "Home";
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            IntervalService = intervalService ?? throw new ArgumentNullException(nameof(intervalService));

            EventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            AddEditIntervalTemplateCommand = new DelegateCommand<object>(async (item) => await AddEditTemplateAsync(item));
            OpenTimerCommand = new DelegateCommand<IntervalTemplate>(async (item) => await OpenTimerAsync(item));

            EventAggregator.GetEvent<IntervalTemplateEvent>().Subscribe(async (item) => await OnIntervalTemplateChangeAsync(item));
        }
        #endregion Constructor

        #region Methods
        public async Task InitializeAsync(INavigationParameters parameters)
        {
            foreach (var template in await IntervalService.GetItemsAsync())
            {
                IntervalTemplates.Add(Mapper.Map<IntervalTemplate>(template));
            }
        }

        private async Task OpenTimerAsync(IntervalTemplate item)
        {
            var nav = new NavigationParameters
            {
                { nameof(IntervalTemplate), item }
            };
            await NavigationService.NavigateAsync("TimerPage", nav, useModalNavigation: true);
        }

        private async Task AddEditTemplateAsync(object obj)
        {
            var nav = new NavigationParameters();
            if(obj is IntervalTemplate item)
            {
                nav.Add(nameof(IntervalTemplate), item); 
            }
            else
            {
                nav.Add(nameof(IntervalTemplate), new IntervalTemplate());
            }
            await NavigationService.NavigateAsync("IntervalTemplatePage", nav, useModalNavigation: true);
        }

        private async Task OnIntervalTemplateChangeAsync(IntervalTemplate item)
        {
            if (item.IsNew)
            {
                await IntervalService.AddItemAsync(Mapper.Map<IntervalTemplateDto>(item));
                IntervalTemplates.Insert(0, item);
            }
            else
            {
                await IntervalService.UpdateItemAsync(Mapper.Map<IntervalTemplateDto>(item));
            }
        }

        public override void Destroy()
        {
            EventAggregator.GetEvent<IntervalTemplateEvent>().Unsubscribe(async (item) => await OnIntervalTemplateChangeAsync(item));
            base.Destroy();
        }
        #endregion Methods
    }
}
