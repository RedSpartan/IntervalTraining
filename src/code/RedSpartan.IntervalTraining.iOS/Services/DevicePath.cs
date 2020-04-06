using System;
using RedSpartan.IntervalTraining.iOS.Services;
using RedSpartan.IntervalTraining.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(DevicePath))]
namespace RedSpartan.IntervalTraining.iOS.Services
{
    public class DevicePath : IDevicePath
    {
        public string Path => GetPath();

        private static string GetPath()
        {
            var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            System.Diagnostics.Debug.WriteLine(path);
            return path;
        }
    }
}
