using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using RedSpartan.IntervalTraining.Repository.DTOs;
using RedSpartan.IntervalTraining.Repository.Services;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Events;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
        private IPageDialogService DialogService { get; }
        #endregion Services

        #region Commands
        public ICommand AddEditIntervalTemplateCommand { get; }
        public ICommand OpenTimerCommand { get; }
        public ICommand DeleteTemplateCommand { get; }
        #endregion Commands

        #region Constructor
        public HomeViewModel(INavigationService navigationService,
            IDataService<IntervalTemplateDto> intervalService,
            IEventAggregator eventAggregator,
            IPageDialogService dialogService,
            IMapper mapper)
            : base(navigationService)
        {
            Title = "Home";
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            IntervalService = intervalService ?? throw new ArgumentNullException(nameof(intervalService));
            DialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            EventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            AddEditIntervalTemplateCommand = new DelegateCommand<object>(async (item) => await AddEditTemplateAsync(item));
            OpenTimerCommand = new DelegateCommand<IntervalTemplate>(async (item) => await OpenTimerAsync(item));
            DeleteTemplateCommand = new DelegateCommand<object>(async (item) => await DeleteTemplateAsync(item));

            EventAggregator.GetEvent<IntervalTemplateEvent>().Subscribe(async (item) => await OnIntervalTemplateChangeAsync(item));
            EventAggregator.GetEvent<HistoryEvent>().Subscribe(OnHistoryEvent);
        }
        #endregion Constructor

        #region Methods
        public async Task InitializeAsync(INavigationParameters parameters)
        {
            foreach (var template in await IntervalService.GetItemsAsync())
            {
                var map = Mapper.Map<IntervalTemplate>(template);

                foreach (var interval in template.Intervals)
                {
                    map.Intervals.Add(Mapper.Map<Interval>(interval));
                }

                foreach (var history in template.History)
                {
                    map.History.Add(Mapper.Map<History>(history));
                }

                IntervalTemplates.Add(map);
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
            if (obj is IntervalTemplate item)
            {
                nav.Add(nameof(IntervalTemplate), item);
            }
            else
            {
                nav.Add(nameof(IntervalTemplate), new IntervalTemplate());
            }
            await NavigationService.NavigateAsync("IntervalTemplatePage", nav, useModalNavigation: true);
        }

        private async Task DeleteTemplateAsync(object obj)
        {
            if (obj is IntervalTemplate item)
            {
                if (Device.RuntimePlatform != Device.UWP
                    && !(await DialogService.DisplayAlertAsync("Delete", $"You are about to delete template '{item.Name}', are you sure?", "Ok", "Cancel")))
                {
                    return;
                }

                if (await IntervalService.DeleteItemAsync(item.Id))
                {
                    IntervalTemplates.Remove(item);
                }
            }
        }

        private async Task OnIntervalTemplateChangeAsync(IntervalTemplate item)
        {
            if (item.IsNew)
            {
                var id = await IntervalService.AddItemAsync(Mapper.Map<IntervalTemplateDto>(item));
                item.Id = id;
                IntervalTemplates.Insert(0, item);
            }
            else
            {
                await IntervalService.UpdateItemAsync(Mapper.Map<IntervalTemplateDto>(item));
            }
        }

        private void OnHistoryEvent(History item)
        {
            var template = IntervalTemplates.FirstOrDefault(x => x.Id == item.TemplateId);

            if (template != null)
            {
                template.History.Add(item);
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
