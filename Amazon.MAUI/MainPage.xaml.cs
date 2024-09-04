using Amazon.MAUI.ViewModels;
namespace Amazon.MAUI
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
        private void InventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Inventory");

        }

        private void ShoppingClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//CartListView");

        }

        private void ExitClicked(object sender, EventArgs e)
        {
            Environment.Exit(0);

        }
    }

}
