using Prism.Mvvm;
using System;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Models
{
    public class Interval : BindableBase
    {
        #region Fields
        private int _order;
        private string _name;
        private TimeSpan _time;
        #endregion Fields

        #region Properties
        public int Order { get => _order; set => SetProperty( ref _order, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public TimeSpan Time { get => _time; set => SetProperty(ref _time, value); }
        #endregion Properties
    }
}
