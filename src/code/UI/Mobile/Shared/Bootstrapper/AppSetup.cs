using Prism.Ioc;
using RedSpartan.IntervalTraining.Common.Interfaces;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Resources;
using System.Threading.Tasks;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Bootstrapper
{
    internal static class AppSetup
    {
        public static async Task InitiliseAsync(IContainerProvider container)
        {
            await InitilisePersistence(container);
            await LicenceCheck();
        }

        private static async Task InitilisePersistence(IContainerProvider container)
        {
            await Repository.Bootstrapper.InitialiseAsync(container.Resolve<IDeviceSpecifics>().DbPath);
        }

        private static async Task LicenceCheck()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(await Licences.GetAsync("syncfusion"));
        }
    }
}
