﻿using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Events;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels
{
    public class IntervalTemplateViewModel : ViewModelBase
    {
        #region Fields
        private string _intervalName;
        private int? _intervalTimeSeconds;
        private IntervalTemplate _template;
        private bool _isBusy;
        #endregion Fields

        #region Properties
        public IntervalTemplate Template
        {
            get => _template;
            set => SetProperty(ref _template, value);
        }

        public string IntervalName
        {
            get => _intervalName;
            set => SetProperty(ref _intervalName, value);
        }

        public int? IntervalTimeSeconds
        {
            get => _intervalTimeSeconds;
            set => SetProperty(ref _intervalTimeSeconds, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        #endregion Properties

        #region Services
        private IntervalTemplateEvent IntervalTemplateEvent { get; }
        #endregion Services

        #region Commands
        public ICommand AddNewIntervalTemplateCommand { get; }
        public ICommand AddNewIntervalCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion Commands

        public IntervalTemplateViewModel(INavigationService navigationService,
            IEventAggregator eventAggregator)
            : base(navigationService)
        {
            Title = "Create New Interval";

            IntervalTemplateEvent = eventAggregator?.GetEvent<IntervalTemplateEvent>()
                ?? throw new ArgumentNullException(nameof(eventAggregator));

            AddNewIntervalTemplateCommand = new DelegateCommand(async () => await AddNewIntervalTemplate());
            AddNewIntervalCommand = new DelegateCommand(async () => await AddNewInterval());
            CancelCommand = new DelegateCommand(async () => await NavigationService.GoBackAsync());
        }

        #region Methods
        private async Task AddNewIntervalTemplate()
        {
            IsBusy = true;
            IntervalTemplateEvent.Publish(Template);
            await NavigationService.GoBackAsync();
            IsBusy = false;
        }

        private Task AddNewInterval()
        {
            return Task.Run(() =>
            {
                IsBusy = true;
                var interval = new Interval 
                { 
                    Name = IntervalName, 
                    Time = TimeSpan.FromSeconds((int)IntervalTimeSeconds),
                    Order = Template.Intervals.Count
                };
                
                Device.BeginInvokeOnMainThread(() => Template.Intervals.Add(interval));
                
                IntervalName = null;
                IntervalTimeSeconds = null;
                IsBusy = false;
            });
        }

        public override void Initialize(INavigationParameters parameters)
        {
            if (!parameters.ContainsKey(nameof(IntervalTemplate)))
            {
                throw new ArgumentNullException(nameof(IntervalTemplate));
            }

            Template = parameters.GetValue<IntervalTemplate>(nameof(IntervalTemplate));
            base.Initialize(parameters);
        }
        #endregion Methods
    }
}