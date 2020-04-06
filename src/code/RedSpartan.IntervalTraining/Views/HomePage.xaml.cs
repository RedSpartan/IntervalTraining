
using RedSpartan.IntervalTraining.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedSpartan.IntervalTraining.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public readonly HomeViewModel _viewModel;
        public HomePage()
        {
            InitializeComponent();
            if(BindingContext is HomeViewModel viewModel)
            {
                _viewModel = viewModel;
            }
            else
            {
                throw new ArgumentNullException(nameof(viewModel));
            }
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewIntervalTemplatePage()));
        }
    }
}