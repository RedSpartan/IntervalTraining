using Prism;
using Prism.Ioc;
using Syncfusion.ListView.XForms.UWP;

namespace RedSpartan.IntervalTraining.UI.Mobile.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            SfListViewRenderer.Init();
            LoadApplication(new Shared.App(new UwpInitializer()));
        }
    }

    public class UwpInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
