using RedSpartan.IntervalTraining.Repository.DTOs;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RedSpartan.IntervalTraining.ViewModels
{
    public class NewIntervalTemplateViewModel : BaseViewModel
    {
        #region Fields
        private string _name;
        private int? _timeSeconds;
        private int? _iterations;
        private string _intervalName;
        private int _intervalTimeSeconds;
        #endregion Fields

        #region Properties
        public string Name 
        { 
            get => _name; 
            set => SetProperty(ref _name, value); 
        }

        public int? TimeSeconds
        {
            get => _timeSeconds; 
            set => SetProperty(ref _timeSeconds, value, ()=> { if(value != null) Iterations = null; });
        }
        
        public int? Iterations 
        { 
            get => _iterations; 
            set => SetProperty(ref _iterations, value, () => { if(value != null) TimeSeconds = null; }); 
        }

        public string IntervalName 
        { 
            get => _intervalName; 
            set => SetProperty(ref _intervalName, value); 
        }

        public int IntervalTimeSeconds 
        { 
            get => _intervalTimeSeconds; 
            set => SetProperty(ref _intervalTimeSeconds, value); 
        }
        #endregion Properties

        #region Collections
        public ObservableCollection<IntervalDto> Intervals { get; set; } = new ObservableCollection<IntervalDto>();
        #endregion Collections

        #region Services
        public IMessagingCenter MessageCenter { get; }
        #endregion Services

        #region Commands
        public ICommand AddNewIntervalTemplateCommand { get; }
        #endregion Commands

        public NewIntervalTemplateViewModel(IMessagingCenter messageCenter)
        {
            Title = "Create New Interval";
            MessageCenter = messageCenter ?? throw new ArgumentNullException(nameof(messageCenter));
            AddNewIntervalTemplateCommand = new Command(async () => await AddNewIntervalTemplate());
        }

        #region Methods
        private async Task AddNewIntervalTemplate()
        {

        }
        #endregion Methods
    }
}
