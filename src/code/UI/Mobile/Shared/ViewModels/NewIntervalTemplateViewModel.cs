using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Events;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class NewIntervalTemplateViewModel : ViewModelBase
    {
        #region Fields
        private string _name;
        private int? _timeSeconds;
        private int? _iterations;
        private string _intervalName;
        private int? _intervalTimeSeconds;
        #endregion Fields

        #region Properties
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public int? TimeSeconds
        {
            get => _timeSeconds;
            set => SetProperty(ref _timeSeconds, value, () => { if (value != null) Iterations = null; });
        }

        public int? Iterations
        {
            get => _iterations;
            set => SetProperty(ref _iterations, value, () => { if (value != null) TimeSeconds = null; });
        }

        public string IntervalName
        {
            get => _intervalName;
            set => SetProperty(ref _intervalName, value);
        }

        public int? IntervalTimeSeconds
        {
            get => _intervalTimeSeconds;
            set => SetProperty(ref _intervalTimeSeconds, value);
        }
        #endregion Properties

        #region Collections
        public ObservableCollection<Interval> Intervals { get; set; } = new ObservableCollection<Interval>();
        #endregion Collections

        #region Services
        private IntervalTemplateEvent IntervalTemplateEvent { get; }
        #endregion Services

        #region Commands
        public ICommand AddNewIntervalTemplateCommand { get; }
        public ICommand AddNewIntervalCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion Commands

        public NewIntervalTemplateViewModel(INavigationService navigationService,
            IEventAggregator eventAggregator)
            : base(navigationService)
        {
            Title = "Create New Interval";

            IntervalTemplateEvent = eventAggregator?.GetEvent<IntervalTemplateEvent>()
                ?? throw new ArgumentNullException(nameof(eventAggregator));

            AddNewIntervalTemplateCommand = new DelegateCommand(async () => await AddNewIntervalTemplate());
            AddNewIntervalCommand = new DelegateCommand(AddNewInterval);
            CancelCommand = new DelegateCommand(async () => await NavigationService.GoBackAsync());
        }

        #region Methods
        private async Task AddNewIntervalTemplate()
        {
            var model = new IntervalTemplate
            {
                Name = Name,
                TimeSeconds = TimeSeconds,
                Iterations = Iterations,
                Intervals = Intervals
            };

            IntervalTemplateEvent.Publish(model);
            await NavigationService.GoBackAsync();
        }

        private void AddNewInterval()
        {
            Intervals.Add(new Interval { Name = IntervalName, TimeSeconds = (int)IntervalTimeSeconds });
            IntervalName = null;
            IntervalTimeSeconds = null;
        }
        #endregion Methods
    }
}
