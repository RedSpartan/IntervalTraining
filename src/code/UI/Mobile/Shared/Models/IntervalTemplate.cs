using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Models
{
    public class IntervalTemplate : BindableBase, IDisposable
    {
        #region Fields
        private int _id;
        private string _name;
        private int? _timeSeconds;
        private int? _iterations;
        #endregion Fields

        #region Properties
        public int Id { get => _id; set => SetProperty(ref _id, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        public int? TimeSeconds 
        { 
            get => _timeSeconds; 
            set => SetProperty(ref _timeSeconds, value, () => RaisePropertyChanged(nameof(IntervalType))); 
        }
        
        public int? Iterations 
        { 
            get => _iterations; 
            set => SetProperty(ref _iterations, value, () => RaisePropertyChanged(nameof(IntervalType))); 
        }

        public bool IsNew => Id == 0;
        public int TotalIntervals => Intervals.Count;
        public int Usage => History.Count;
        public string IntervalType => GetIntervalType();
        #endregion Properties

        #region Collections
        public ObservableCollection<Interval> Intervals { get; }

        public ObservableCollection<History> History { get; }
        #endregion Collections

        public IntervalTemplate()
        {
            Intervals = new ObservableCollection<Interval>();
            Intervals.CollectionChanged += Intervals_CollectionChanged;

            History = new ObservableCollection<History>();
            History.CollectionChanged += History_CollectionChanged;
        }

        private string GetIntervalType()
        {
            if (TimeSeconds == null && Iterations == null)
            {
                return "∞"; 
            }

            if (Iterations != null)
            {
                return $"#{Iterations}";
            }

            var timeSpan = TimeSpan.FromSeconds((int)TimeSeconds);
            return timeSpan.ToString(@"mm\:ss");
        }

        private void History_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    RaisePropertyChanged(nameof(Usage));
                    break;
            }
        }

        private void Intervals_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    RaisePropertyChanged(nameof(TotalIntervals));
                    break;
            }
        }

        public void Dispose()
        {
            Intervals.CollectionChanged -= Intervals_CollectionChanged;
            History.CollectionChanged -= History_CollectionChanged;
        }
    }
}
