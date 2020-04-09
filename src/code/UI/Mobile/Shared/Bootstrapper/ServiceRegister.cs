using AutoMapper;
using Prism.Ioc;
using RedSpartan.IntervalTraining.Common.Interfaces;
using RedSpartan.IntervalTraining.Internal.Repository.Access;
using RedSpartan.IntervalTraining.Repository.Configuration;
using RedSpartan.IntervalTraining.Repository.DTOs;
using RedSpartan.IntervalTraining.Repository.Services;
using Xamarin.Forms;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Bootstrapper
{
    internal static class ServiceRegister
    {
        internal static IContainerRegistry RegisterServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDataService<HistoryDto>, HistoryDataService>();
            containerRegistry.Register<IDataService<IntervalTemplateDto>, IntervalDataService>();
            
            containerRegistry.Register<IContextFactory, ContextFactory>();

            containerRegistry.RegisterInstance(DependencyService.Get<IDeviceSpecifics>());
            containerRegistry.RegisterInstance<IMapper>(new Mapper(new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<AutoMapperModelProfile>();
                cfg.AddProfile<AutoMapperRepoProfile>();
            })));

            return containerRegistry;
        }
        
    }
}
