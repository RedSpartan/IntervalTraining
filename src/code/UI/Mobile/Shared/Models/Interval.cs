using Prism.Mvvm;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Models
{
    public class Interval : BindableBase
    {
        #region Fields
        private int _order;
        private string _name;
        private int _timeSeconds;
        #endregion Fields

        #region Properties
        public int Order { get => _order; set => SetProperty( ref _order, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public int TimeSeconds { get => _timeSeconds; set => SetProperty(ref _timeSeconds, value); }
        #endregion Properties
    }
}
