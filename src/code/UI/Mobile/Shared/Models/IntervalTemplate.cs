using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Models
{
    public class IntervalTemplate : BindableBase
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
        public int? TimeSeconds { get => _timeSeconds; set => SetProperty(ref _timeSeconds, value); }
        public int? Iterations { get => _iterations; set => SetProperty(ref _iterations, value); }
        #endregion Properties

        #region Collections
        public ObservableCollection<Interval> Intervals { get; set; } = new ObservableCollection<Interval>();

        public ObservableCollection<History> History { get; set; } = new ObservableCollection<History>();
        #endregion Collections
    }
}
