using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntervalTemplatePage : ContentPage
    {
        public IntervalTemplatePage()
        {
            InitializeComponent();
        }
        private void listView_ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            switch (e.Action)
            {
                case DragAction.Start:
                    if (e.ItemData is Interval startModel)
                    {
                        startModel.InMotion = true;
                    }
                    break;
                case DragAction.Drop:
                    if (e.ItemData is Interval dropModel)
                    {
                        dropModel.InMotion = false;
                    }
                    break;
            }
        }
    }
}