using RedSpartan.IntervalTraining.Common.Interfaces;
using RedSpartan.IntervalTraining.UI.Mobile.UWP.Services;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceSpecifics))]
namespace RedSpartan.IntervalTraining.UI.Mobile.UWP.Services
{
    public class DeviceSpecifics : IDeviceSpecifics
    {
        public string Path => GetPath();
        
        public string DbPath => System.IO.Path.Combine(Path, Shared.App.DB_NAME);
        
        private static string GetPath()
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Path: {ApplicationData.Current.LocalFolder.Path}");
#endif
            return ApplicationData.Current.LocalFolder.Path;
        }
    }
}
