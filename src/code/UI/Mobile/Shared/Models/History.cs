using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Models
{
    public class History : BindableBase
    {
        #region Fileds
        private int _id;
        private IntervalTemplate _template;
        private int _timeActiveSeconds;
        private DateTime _start;
        private DateTime _stop;
        private string _name;
        #endregion Fileds

        #region Properties
        public int Id { get => _id; set => SetProperty(ref _id, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public IntervalTemplate Template { get => _template; set => SetProperty(ref _template, value); }
        public int TimeActiveSeconds { get => _timeActiveSeconds; set => SetProperty(ref _timeActiveSeconds, value); }
        public DateTime Start { get => _start; set => SetProperty(ref _start, value); }
        public DateTime Stop { get => _stop; set => SetProperty(ref _stop, value); }
        #endregion Properties

        #region Collections
        public ObservableCollection<Interval> Intervals { get; set; } = new ObservableCollection<Interval>();
        #endregion Collections
    }
}
