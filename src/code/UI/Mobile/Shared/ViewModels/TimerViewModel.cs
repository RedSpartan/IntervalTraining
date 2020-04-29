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
        private string _timeRemaining = "00:00.0";
        private bool _started = false;
        private bool _finished = false;
        private string _finaliseButtonLabel = "Close";
        private double _percentageComplete = 100;
        private Color _evenColour;
        private Color _oddColour;
        private bool _clockwise;
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

        public double PercentageComplete
        {
            get => _percentageComplete;
            set  
            { 
                if((_percentageComplete == 100 && value == 0)
                    || (_percentageComplete == 0 && value == 100))
                {
                    return;
                }
                SetProperty(ref _percentageComplete, value); 
            }
        }

        public string FinaliseButtonLabel
        {
            get => _finaliseButtonLabel;
            set => SetProperty(ref _finaliseButtonLabel, value);
        }

        public Color EvenColour 
        { 
            get => _evenColour; 
            set => SetProperty(ref _evenColour, value); 
        }

        public Color OddColour 
        { 
            get => _oddColour; 
            set => SetProperty(ref _oddColour, value); 
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
        public ICommand FinaliseCommand { get; }
        #endregion Commands

        #region Constructors
        public TimerViewModel(INavigationService navigationService,
            IEventAggregator eventAggregator)
            : base(navigationService)
        {
            EventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            StartCommand = new DelegateCommand(Start);
            FinaliseCommand = new DelegateCommand(Finalise);

            if(Application.Current.Resources.TryGetValue("Colour-Blue", out var blue))
            {
                EvenColour = (Color)blue;
            }
            if (Application.Current.Resources.TryGetValue("Colour-Red", out var red))
            {
                OddColour = (Color)red;
            }
        }
        #endregion Constructors

        #region Public Methods
        public override void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(nameof(IntervalTemplate)))
            {
                IntervalTemplate = parameters.GetValue<IntervalTemplate>(nameof(IntervalTemplate));
            }
            base.OnNavigatedTo(parameters);

            CreateQueue(IntervalTemplate);

            SetupTimer();

            Timer.TimeChanged += () =>
            {
                TimeRemaining = Timer.TimeLeftMsStr;

                if (_clockwise)
                {
                    PercentageComplete = 100 - Timer.PercentageComplete;
                }
                else
                {
                    PercentageComplete = Timer.PercentageComplete;
                }
            };

            Timer.CountDownFinished += () => OnCountDownFinish();

            Timer.StepMs = 33;

            _history.TemplateId = IntervalTemplate.Id;
            _history.Name = IntervalTemplate.Name;
        }

        public override void Destroy()
        {
            Timer.Dispose();
            base.Destroy();
        }
        #endregion Public Methods

        #region Private Methods
        private void OnCountDownFinish()
        {
            _history.TimeActiveSeconds += (int)Queue.Peek().Time.TotalSeconds;
            _compleatedIntervals.Push(Queue.Peek().Clone());

            if (IntervalTemplate.Iterations == null && IntervalTemplate.TimeSeconds == null)
            {
                Queue.Enqueue(Queue.Peek());
            }

            Queue.Dequeue();

            if (_finished || Queue.Count == 0)
            {
                _started = false;
                _finished = true;
                FinaliseButtonLabel = "Finish";
            }
            else
            {
                SetupTimer();
            }
        }

        private void Start()
        {
            if (_history.Start == DateTime.MinValue)
            {
                _history.Start = DateTime.UtcNow;
            }
            _started = true;
            Timer.Start();
            FinaliseButtonLabel = "Stop";
        }

        private async void Finalise()
        {
            switch (FinaliseButtonLabel)
            {
                case "Stop":
                    Timer.Pause();
                    _started = false;
                    FinaliseButtonLabel = "Finish";
                    break;
                case "Finish":
                    FinaliseHistory();
                    await NavigationService.GoBackAsync();
                    break;
                default:
                    await NavigationService.GoBackAsync();
                    break;
            }
        }

        private void SetupTimer()
        {
            var timespan = Queue.Peek().Time;
            Timer.SetTime(new DateTime(1, 1, 1, 0, timespan.Minutes, timespan.Seconds));

            TimeRemaining = Timer.TimeLeftMsStr;

            if (_started)
            {
                Timer.Start();
            }
            SetAlternative();
        }

        private void SetAlternative()
        {
            if (_compleatedIntervals.Count % 2 == 0)
            {
                _clockwise = false;
                /*if (Application.Current.Resources.TryGetValue("Colour-Blue", out var blue))
                {
                    EvenColour = (Color)blue;
                }
                if (Application.Current.Resources.TryGetValue("Colour-Red", out var red))
                {
                    OddColour = (Color)red;
                }*/
            }
            else
            {
                _clockwise = true;
                /*if (Application.Current.Resources.TryGetValue("Colour-Red", out var red))
                {
                    EvenColour = (Color)red;
                }
                if (Application.Current.Resources.TryGetValue("Colour-Blue", out var blue))
                {
                    OddColour = (Color)blue;
                }*/
            }
        }

        private void FinaliseHistory()
        {
            _history.Stop = DateTime.UtcNow;
            _history.Intervals = new List<Interval>(_compleatedIntervals);
            EventAggregator.GetEvent<HistoryEvent>().Publish(_history);
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
            else if (template.TimeSeconds != null)
            {
                double totalTime = 0;
                while (totalTime < template.TimeSeconds)
                {
                    foreach (var item in template.Intervals)
                    {
                        totalTime += item.Time.TotalSeconds;
                        Queue.Enqueue(item);

                        if (totalTime >= template.TimeSeconds)
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
        #endregion Private Methods
    }
}