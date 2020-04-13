using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Models
{
    public class History : BindableBase
    {
        #region Fileds
        private int _id;
        private int? _templateId;
        private int _timeActiveSeconds;
        private DateTime _start;
        private DateTime _stop;
        private string _name;
        #endregion Fileds

        #region Properties
        public int Id { get => _id; set => SetProperty(ref _id, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public int? TemplateId { get => _templateId; set => SetProperty(ref _templateId, value); }
        public int TimeActiveSeconds { get => _timeActiveSeconds; set => SetProperty(ref _timeActiveSeconds, value); }
        public DateTime Start { get => _start; set => SetProperty(ref _start, value); }
        public DateTime Stop { get => _stop; set => SetProperty(ref _stop, value); }
        #endregion Properties

        #region Collections
        public List<Interval> Intervals { get; set; } = new List<Interval>();
        #endregion Collections
    }
}
