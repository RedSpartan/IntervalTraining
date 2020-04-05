using Xamarin.Forms;
using RedSpartan.IntervalTraining.Services;
using RedSpartan.IntervalTraining.Bootstrap;

namespace RedSpartan.IntervalTraining
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            AppContainer.Container = new AppSetup().CreateContainer();
            DependencyService.Register<MockDataStore>();
            /*DependencyService.Register<IDataService<HistoryDto>, HistoryDataService>();
            DependencyService.Register<IDataService<IntervalTemplateDto>, IntervalDataService>();
            DependencyService.Register<DatabaseContext>();
            DependencyService.Register<IMapper, Mapper>();*/

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
