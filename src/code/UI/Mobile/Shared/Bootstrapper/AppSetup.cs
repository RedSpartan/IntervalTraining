using Prism.Ioc;
using RedSpartan.IntervalTraining.Common.Interfaces;
using System.Threading.Tasks;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Bootstrapper
{
    internal static class AppSetup
    {
        public static async Task InitiliseAsync(IContainerProvider container)
        {
            await InitilisePersistence(container);
        }

        private static async Task InitilisePersistence(IContainerProvider container)
        {
            await Repository.Bootstrapper.InitialiseAsync(container.Resolve<IDeviceSpecifics>().DbPath);
        }
    }
}
