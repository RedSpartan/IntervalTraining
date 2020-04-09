using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using RedSpartan.IntervalTraining.Common;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;
using System;
using System.Windows.Input;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class TimerViewModel : ViewModelBase
    {
        #region Fields
        private IntervalTemplate _intervalTemplate;
        private CountDownTimer _timer = new CountDownTimer();
        private string _timeRemaining = "00:00.000";
        #endregion  Fields

        #region Properties
        public IntervalTemplate IntervalTemplate
        {
            get => _intervalTemplate;
            set => SetProperty(ref _intervalTemplate, value);
        }

        public CountDownTimer Timer
        {
            get => _timer;
            set => SetProperty(ref _timer, value);
        }

        public string TimeRemaining
        {
            get => _timeRemaining;
            set => SetProperty(ref _timeRemaining, value);
        }
        #endregion Properties

        #region Services
        public IEventAggregator EventAggregator { get; }
        #endregion Services

        #region Commands
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        #endregion Commands

        #region Constructor
        public TimerViewModel(INavigationService navigationService,
            IEventAggregator eventAggregator)
            : base(navigationService)
        {
            EventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            StartCommand = new DelegateCommand(() => Timer.Start());
            StopCommand = new DelegateCommand(() => Timer.Pause());
        }
        #endregion Constructor

        #region Methods
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(nameof(IntervalTemplate)))
            {
                IntervalTemplate = parameters.GetValue<IntervalTemplate>(nameof(IntervalTemplate));
            }
            base.OnNavigatedTo(parameters);

            Timer.SetTime(0, 10);

            Timer.TimeChanged += () => TimeRemaining = Timer.TimeLeftMsStr;

            Timer.CountDownFinished += () => TimeRemaining = "FINISHED";

            Timer.StepMs = 33;
        }

        public override void Destroy()
        {
            Timer.Dispose();
            base.Destroy();
        }
        #endregion Methods
    }
}
