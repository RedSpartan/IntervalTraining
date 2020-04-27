using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Events;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class IntervalTemplateViewModel : ViewModelBase
    {
        #region Fields
        private string _intervalName;
        private int? _intervalTimeSeconds;
        private IntervalTemplate _template;
        private bool _isBusy;
        private int _selectedIndex = 0;
        #endregion Fields

        #region Properties
        private IntervalTemplate InitialState { get; set; }

        public int SelectedIndex { get => _selectedIndex; set => SetProperty(ref _selectedIndex, value); }

        public IntervalTemplate Template
        {
            get => _template;
            set => SetProperty(ref _template, value);
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

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        #endregion Properties

        #region Services
        private IntervalTemplateEvent IntervalTemplateEvent { get; }
        #endregion Services

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand AddIntervalCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteTemplateCommand { get; }
        #endregion Commands

        #region Constructors
        public IntervalTemplateViewModel(INavigationService navigationService,
            IEventAggregator eventAggregator)
            : base(navigationService)
        {
            Title = "Create New Interval";

            IntervalTemplateEvent = eventAggregator?.GetEvent<IntervalTemplateEvent>()
                ?? throw new ArgumentNullException(nameof(eventAggregator));

            SaveCommand = new DelegateCommand(async () => await Save(), CanSave)
                .ObservesProperty(() => Template.TotalIntervals)
                .ObservesProperty(() => Template.Name);

            AddIntervalCommand = new DelegateCommand(async () => await AddInterval(), CanAddInterval)
                .ObservesProperty(() => IntervalName)
                .ObservesProperty(() => IntervalTimeSeconds);

            CancelCommand = new DelegateCommand(async () => await Cancel());

            DeleteTemplateCommand = new DelegateCommand<Interval>(DeleteIntervalFromIntervals);
        }
        #endregion Constructors

        #region Public Methods
        public override void Initialize(INavigationParameters parameters)
        {
            if (!parameters.ContainsKey(nameof(IntervalTemplate)))
            {
                throw new ArgumentNullException(nameof(IntervalTemplate));
            }

            Template = parameters.GetValue<IntervalTemplate>(nameof(IntervalTemplate));
            InitialState = Template.Clone();
            foreach (var interval in Template.Intervals)
            {
                InitialState.Intervals.Add(interval);
            }
            base.Initialize(parameters);
        }
        #endregion Public Methods

        #region Private Methods
        private async Task Save()
        {
            IsBusy = true;
            IntervalTemplateEvent.Publish(Template);
            await NavigationService.GoBackAsync();
            IsBusy = false;
        }

        private bool CanSave()
        {
            return Template?.TotalIntervals > 0 && !string.IsNullOrEmpty(Template?.Name);
        }

        private void DeleteIntervalFromIntervals(Interval item)
        {
            Template.Intervals.Remove(item);
        }

        private Task AddInterval()
        {
            return Task.Run(() =>
            {
                IsBusy = true;
                var interval = new Interval
                {
                    Name = IntervalName,
                    Time = TimeSpan.FromSeconds((int)IntervalTimeSeconds),
                    Order = Template.Intervals.Count
                };

                Device.BeginInvokeOnMainThread(() => Template.Intervals.Add(interval));

                IntervalName = null;
                IntervalTimeSeconds = null;
                IsBusy = false;
            });
        }

        private bool CanAddInterval()
        {
            return !string.IsNullOrWhiteSpace(IntervalName) && IntervalTimeSeconds != null;
        }

        private async Task Cancel()
        {
            InitialState.CopyTo(Template);
            Template.Intervals.Clear();
            foreach (var interval in InitialState.Intervals)
            {
                Template.Intervals.Add(interval);
            }
            await NavigationService.GoBackAsync();
        }
        #endregion Private Methods
    }
}