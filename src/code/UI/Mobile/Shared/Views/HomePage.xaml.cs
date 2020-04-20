using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.ViewModels;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void listView_ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            switch (e.Action)
            {
                case DragAction.Start:
                    if(e.ItemData is IntervalTemplate startModel)
                    {
                        startModel.InMotion = true;
                    }
                    break;
                case DragAction.Drop:
                    if (e.ItemData is IntervalTemplate dropModel)
                    {
                        dropModel.InMotion = false;
                    }
                    break;
            }
        }
    }
}