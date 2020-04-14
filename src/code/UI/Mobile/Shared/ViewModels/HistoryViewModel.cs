using AutoMapper;
using Prism.Events;
using Prism.Navigation;
using RedSpartan.IntervalTraining.Repository.DTOs;
using RedSpartan.IntervalTraining.Repository.Services;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Events;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class HistoryViewModel : ViewModelBase, IInitializeAsync
    {
        #region Collections
        public ObservableCollection<History> History { get; } = new ObservableCollection<History>();
        #endregion Collections

        #region Services
        public IEventAggregator EventAggregator { get; }
        public IDataService<HistoryDto> HistoryService { get; }
        private IMapper Mapper { get; }
        #endregion Services

        #region Constructors
        public HistoryViewModel(INavigationService navigationService,
            IDataService<HistoryDto> historyService,
            IMapper mapper,
            IEventAggregator eventAggregator) 
            : base(navigationService)
        {
            Title = "History";
            EventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            HistoryService = historyService ?? throw new ArgumentNullException(nameof(historyService));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            EventAggregator.GetEvent<HistoryEvent>().Subscribe(async (item) => await AddHistoryRecordAsync(item));
        }
        #endregion Constructors

        #region Methods
        private async Task AddHistoryRecordAsync(History item)
        {
            var id = await HistoryService.AddItemAsync(Mapper.Map<HistoryDto>(item));
            item.Id = id;

            Device.BeginInvokeOnMainThread(() => {
                History.Insert(0, item);
            });
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var collection = await HistoryService.GetItemsAsync();
            foreach (var item in collection)
            {
                History.Add(Mapper.Map<History>(item));
            }
        }

        public override void Destroy()
        {
            EventAggregator.GetEvent<HistoryEvent>().Unsubscribe(async (item) => await AddHistoryRecordAsync(item));
            base.Destroy();
        }
        #endregion Methods
    }
}
