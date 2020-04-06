using Autofac;
using AutoMapper;
using RedSpartan.IntervalTraining.Models;
using RedSpartan.IntervalTraining.Repository;
using RedSpartan.IntervalTraining.Repository.DTOs;
using RedSpartan.IntervalTraining.Repository.MappingProfiles;
using RedSpartan.IntervalTraining.Repository.Services;
using RedSpartan.IntervalTraining.Services;
using RedSpartan.IntervalTraining.ViewModels;
using Xamarin.Forms;

namespace RedSpartan.IntervalTraining.Bootstrap
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);
            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder cb)
        {
            DependencyService.Register<IDataStore<Item>, MockDataStore>();

            cb.RegisterType<AboutViewModel>();
            cb.RegisterType<ItemsViewModel>();
            cb.RegisterType<ItemsViewModel>();

            cb.RegisterType<DatabaseContext>();

            cb.RegisterType<MockDataStore>().As<IDataStore<Item>>();
            cb.RegisterType<HistoryDataService>().As<IDataService<HistoryDto>>();
            cb.RegisterType<IntervalDataService>().As<IDataService<IntervalTemplateDto>>();
            
            cb.RegisterInstance(DependencyService.Get<IDevicePath>()).As<IDevicePath>().SingleInstance();
            cb.RegisterInstance(new Mapper(AuroMapperConfiguration.MapperConfiguration())).As<IMapper>().SingleInstance();
        }
    }
}
