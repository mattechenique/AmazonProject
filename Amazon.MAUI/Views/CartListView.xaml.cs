using Amazon.Library.Models;
using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views
{
    public partial class CartListView : ContentPage
    {
        public CartListView()
        {
            InitializeComponent();
        }

        private void AddNewCartClicked(object sender, EventArgs e)
        {
            (BindingContext as CartListViewModel)?.AddNewCart();
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}
