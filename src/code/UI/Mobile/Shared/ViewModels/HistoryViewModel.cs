using Prism.Events;
using Prism.Navigation;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Events;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;
using System;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class HistoryViewModel : ViewModelBase
    {
        public IEventAggregator EventAggregator { get; set; }

        public HistoryViewModel(INavigationService navigationService,
            IEventAggregator eventAggregator) 
            : base(navigationService)
        {
            Title = "History";
            EventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            EventAggregator.GetEvent<CreateHistoryEvent>().Subscribe(AddHistoryRecord);
        }

        private void AddHistoryRecord(History obj)
        {
            throw new NotImplementedException();
        }
    }
}
