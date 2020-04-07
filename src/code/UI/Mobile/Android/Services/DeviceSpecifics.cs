using System;
using RedSpartan.IntervalTraining.Common.Interfaces;
using RedSpartan.IntervalTraining.UI.Mobile.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceSpecifics))]
namespace RedSpartan.IntervalTraining.UI.Mobile.Droid.Services
{
    public class DeviceSpecifics : IDeviceSpecifics
        {
            public string Path => GetPath();

        public string DbPath => System.IO.Path.Combine(Path, Shared.App.DB_NAME);

        private static string GetPath()
            {
                var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                System.Diagnostics.Debug.WriteLine(path);
                return path;
            }
        }
    }
