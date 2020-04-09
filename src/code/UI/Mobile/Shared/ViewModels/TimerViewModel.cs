using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using RedSpartan.IntervalTraining.Common;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class TimerViewModel : ViewModelBase, IInitializeAsync
    {
        #region Fields
        private IntervalTemplate _intervalTemplate;
        private CountDownTimer _timer = new CountDownTimer();
        private string _timeRemaining = "00:00.000";
        private bool _started = false;
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

        #region Collections
        public ObservableQueue<Interval> Queue { get; } = new ObservableQueue<Interval>();
        #endregion Collections

        #region Services
        public IEventAggregator EventAggregator { get; }
        #endregion Services

        #region Commands
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand CloseCommand { get; }
        
        #endregion Commands

        #region Constructor
        public TimerViewModel(INavigationService navigationService,
            IEventAggregator eventAggregator)
            : base(navigationService)
        {
            EventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            StartCommand = new DelegateCommand(() => { _started = true; Timer.Start(); });
            StopCommand = new DelegateCommand(() => { Timer.Pause(); _started = false; });
            CloseCommand = new DelegateCommand(async () => await NavigationService.GoBackAsync());
        }
        #endregion Constructor

        #region Methods

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(nameof(IntervalTemplate)))
            {
                IntervalTemplate = parameters.GetValue<IntervalTemplate>(nameof(IntervalTemplate));
            }
            base.OnNavigatedTo(parameters);

            CreateQueue(IntervalTemplate);

            SetupTimer();
            
            Timer.TimeChanged += () => TimeRemaining = Timer.TimeLeftMsStr;

            Timer.CountDownFinished += () => OnCountDownFinish();

            Timer.StepMs = 33;
        }

        private void OnCountDownFinish()
        {
            Queue.Dequeue();

            if (Queue.Count > 0)
            {
                SetupTimer();
            }
            else
            {
                _started = false;
                TimeRemaining = "Finished";
            }
        }

        private void SetupTimer()
        {
            Timer.SetTime(0, Queue.Peek().TimeSeconds);

            TimeRemaining = Timer.TimeLeftMsStr;
            
            if (_started)
            {
                Timer.Start();
            }
        }

        private void CreateQueue(IntervalTemplate template)
        {
            if (template.Iterations != null)
            {
                for (int i = 0; i < template.Iterations; i++)
                {
                    foreach (var item in template.Intervals)
                    {
                        Queue.Enqueue(item);
                    }
                }
            }
            else if(template.TimeSeconds != null)
            {
                int totalTime = 0;
                while (totalTime < template.TimeSeconds)
                {
                    foreach (var item in template.Intervals)
                    {
                        totalTime += item.TimeSeconds;
                        Queue.Enqueue(item);

                        if(totalTime >= template.TimeSeconds)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (var item in template.Intervals)
                {
                    Queue.Enqueue(item);
                }
            }
        }

        public override void Destroy()
        {
            Timer.Dispose();
            base.Destroy();
        }
        #endregion Methods
    }
}
