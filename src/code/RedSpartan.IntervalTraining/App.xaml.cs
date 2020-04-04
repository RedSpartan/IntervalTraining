using Xamarin.Forms;
using RedSpartan.IntervalTraining.Services;
using AutoMapper;
using RedSpartan.IntervalTraining.Repository.Services;
using RedSpartan.IntervalTraining.Repository.DTOs;
using RedSpartan.IntervalTraining.Repository;

namespace RedSpartan.IntervalTraining
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IDataService<HistoryDto>, HistoryDataService>();
            DependencyService.Register<IDataService<IntervalTemplateDto>, IntervalDataService>();
            DependencyService.Register<DatabaseContext>();
            DependencyService.Register<IMapper, Mapper>();
            


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
