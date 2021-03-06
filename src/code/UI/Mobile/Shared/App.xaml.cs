﻿using Prism;
using Prism.Ioc;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Bootstrapper;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RedSpartan.IntervalTraining.UI.Mobile.Shared
{
    public partial class App
    {
        public const string DB_NAME = "IntTra.db3";
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await AppSetup.InitiliseAsync(Container);

            await NavigationService.NavigateAsync("/MainPage?selectedPage=HomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPages().RegisterServices();
        }
    }
}
