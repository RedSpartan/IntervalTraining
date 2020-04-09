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
        public ICommand AddNewIntervalTemplateCommand { get; }
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

            AddNewIntervalTemplateCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("NewIntervalTemplatePage", useModalNavigation: true));
            OpenTimerCommand = new DelegateCommand<IntervalTemplate>(async (item) => await OpenTimer(item));

            EventAggregator.GetEvent<CreateIntervalTemplateEvent>().Subscribe(async (item) => await OnNewIntervalTemplate(item));
            EventAggregator.GetEvent<UpdateIntervalTemplateEvent>().Subscribe(async (item) => await OnUpdateIntervalTemplate(item));
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

        private async Task OpenTimer(IntervalTemplate item)
        {
            var nav = new NavigationParameters
            {
                { nameof(IntervalTemplate), item }
            };
            await NavigationService.NavigateAsync("TimerPage", nav, useModalNavigation: true);
        }

        private async Task OnNewIntervalTemplate(IntervalTemplate item)
        {
            await IntervalService.AddItemAsync(Mapper.Map<IntervalTemplateDto>(item));
            IntervalTemplates.Insert(0, item);
        }

        private async Task OnUpdateIntervalTemplate(IntervalTemplate item)
        {
            await IntervalService.UpdateItemAsync(Mapper.Map<IntervalTemplateDto>(item));
        }

        public override void Destroy()
        {
            EventAggregator.GetEvent<CreateIntervalTemplateEvent>().Unsubscribe(async (item) => await OnNewIntervalTemplate(item));
            EventAggregator.GetEvent<UpdateIntervalTemplateEvent>().Unsubscribe(async (item) => await OnUpdateIntervalTemplate(item));
            base.Destroy();
        }
        #endregion Methods
    }
}
