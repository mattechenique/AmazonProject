using Amazon.Library.Models;
using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views
{
    public partial class CartListView : ContentPage
    {
        public CartListView()
        {
            InitializeComponent();
            BindingContext = new CartListViewModel();
        }

        private void OpenCartClicked(object sender, EventArgs e)
        {
            var cartId = (sender as Button)?.CommandParameter as int?;
            if (cartId.HasValue)
            {
                Shell.Current.GoToAsync($"//Shop?cartId={cartId}");
            }
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
