using Xamarin.Forms;
using RedSpartan.IntervalTraining.Services;
using AutoMapper;

namespace RedSpartan.IntervalTraining
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            
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
