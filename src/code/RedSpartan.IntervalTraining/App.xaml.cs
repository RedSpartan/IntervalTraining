using Xamarin.Forms;
using RedSpartan.IntervalTraining.Bootstrap;
using RedSpartan.IntervalTraining.Services;
using Autofac;

namespace RedSpartan.IntervalTraining
{
    public partial class App : Application
    {
        internal const string DB_NAME = "IntervalTraining.db3";
        public App()
        {
            InitializeComponent();
            AppContainer.Container = new AppSetup().CreateContainer();
            //TODO: refactor this
            var device = AppContainer.Container.Resolve<IDevicePath>();
            var path = System.IO.Path.Combine(device.Path, DB_NAME);
            Repository.Bootstrapper.Initialise(path);
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
