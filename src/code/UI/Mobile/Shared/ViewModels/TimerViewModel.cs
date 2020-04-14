using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using RedSpartan.IntervalTraining.Common;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Events;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class TimerViewModel : ViewModelBase
    {
        #region Fields
        private IntervalTemplate _intervalTemplate;
        private CountDownTimer _timer = new CountDownTimer();
        private string _timeRemaining = "00:00.000";
        private bool _started = false;
        private bool _finished = false;
        private readonly History _history = new History();
        private readonly Stack<Interval> _compleatedIntervals = new Stack<Interval>();
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
            StartCommand = new DelegateCommand(Start);
            StopCommand = new DelegateCommand(Stop);
            CloseCommand = new DelegateCommand(async () => await NavigationService.GoBackAsync());
        }
        #endregion Constructor

        #region Methods
        public override void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(nameof(IntervalTemplate)))
            {
                IntervalTemplate = parameters.GetValue<IntervalTemplate>(nameof(IntervalTemplate));
            }
            base.OnNavigatedTo(parameters);

            CreateQueue(IntervalTemplate);

            SetupTimer();

            Timer.TimeChanged += () => TimeRemaining = Timer.TimeLeftStr;

            Timer.CountDownFinished += () => OnCountDownFinish();

            Timer.StepMs = 33;

            _history.TemplateId = IntervalTemplate.Id;
            _history.Name = IntervalTemplate.Name;
        }

        private void OnCountDownFinish()
        {
            _history.TimeActiveSeconds += (int)Queue.Peek().Time.TotalSeconds;
            _compleatedIntervals.Push(Queue.Peek().Clone());
            
            if(IntervalTemplate.Iterations == null && IntervalTemplate.TimeSeconds == null)
            {
                Queue.Enqueue(Queue.Peek());
            }

            Queue.Dequeue();

            if (_finished || Queue.Count == 0)
            {
                _started = false;
                _finished = true;
                _history.Stop = DateTime.UtcNow;
                _history.Intervals = new List<Interval>(_compleatedIntervals);
                EventAggregator.GetEvent<CreateHistoryEvent>().Publish(_history);
            }
            else
            {
                SetupTimer();
            }
        }

        private void Start()
        {
            if(_history.Start == DateTime.MinValue)
            {
                _history.Start = DateTime.UtcNow;
            }
            _started = true; 
            Timer.Start();
        }

        private void Stop()
        {
            Timer.Pause();
            _started = false;
        }

        private void SetupTimer()
        {
            var timespan = Queue.Peek().Time;
            Timer.SetTime(timespan.Minutes, timespan.Seconds);

            TimeRemaining = Timer.TimeLeftStr;
            
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
                double totalTime = 0;
                while (totalTime < template.TimeSeconds)
                {
                    foreach (var item in template.Intervals)
                    {
                        totalTime += item.Time.TotalSeconds;
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
