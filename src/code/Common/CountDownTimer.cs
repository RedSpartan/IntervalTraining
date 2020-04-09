using System;
using System.Timers;

namespace RedSpartan.IntervalTraining.Common
{
    public class CountDownTimer : IDisposable
    {
        #region Fields
        private readonly Timer _timer = new Timer();

        private DateTime _maxTime = new DateTime(1, 1, 1, 0, 30, 0);
        private readonly DateTime _minTime = new DateTime(1, 1, 1, 0, 0, 0);
        #endregion Fields

        #region Properties
        public Action TimeChanged { get; set; }
        public Action CountDownFinished { get; set; }

        public bool IsRunnign => _timer.Enabled;

        public double StepMs
        {
            get => _timer.Interval;
            set => _timer.Interval = value;
        }

        public DateTime TimeLeft { get; private set; }

        private long TimeLeftMs => TimeLeft.Ticks / TimeSpan.TicksPerMillisecond;

        public string TimeLeftStr => TimeLeft.ToString("mm:ss");

        public string TimeLeftMsStr => TimeLeft.ToString("mm:ss.fff");
        #endregion Properties

        #region Constructors
        public CountDownTimer(int min, int sec) : this()
        {
            SetTime(min, sec);
        }

        public CountDownTimer(DateTime dt) : this()
        {
            SetTime(dt);
        }

        public CountDownTimer()
        {
            Init();
        }
        #endregion Constructors

        #region Methods
        private void Init()
        {
            TimeLeft = _maxTime;

            StepMs = 1000;
            _timer.Elapsed += new ElapsedEventHandler(TimerTick);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (TimeLeftMs > _timer.Interval)
            {
                TimeLeft = TimeLeft.AddMilliseconds(-_timer.Interval);
                TimeChanged?.Invoke();
            }
            else
            {
                Stop();
                TimeLeft = _minTime;

                TimeChanged?.Invoke();
                CountDownFinished?.Invoke();
            }
        }

        public void SetTime(DateTime dt)
        {
            TimeLeft = _maxTime = dt;
            TimeChanged?.Invoke();
        }

        public void SetTime(int min, int sec = 0) => SetTime(new DateTime(1, 1, 1, 0, min, sec));

        public void Start() => _timer.Start();

        public void Pause() => _timer.Stop();

        public void Stop()
        {
            Pause();
            Reset();
        }

        public void Reset()
        {
            TimeLeft = _maxTime;
        }

        public void Restart()
        {
            Reset();
            Start();
        }

        public void Dispose() 
        {
            _timer.Elapsed -= new ElapsedEventHandler(TimerTick);
            _timer.Dispose(); 
        }
        #endregion Methods
    }
}
