using Amazon.MAUI.ViewModels;
using Amazon.Library.Services;
using Amazon.Library.Models;

namespace Amazon.MAUI.Views;

public partial class CartView : ContentPage
{
    public CartView()
	{
		InitializeComponent();
        BindingContext = new InventoryViewModel();

    }
    private void OkClicked(object sender, EventArgs e)
    {
        string idText = ItemIdEntry.Text;
        string quantityText = ItemQuantityEntry.Text;
        int id, quantity;
        bool idConversion = int.TryParse(idText, out id);
        bool quantityConversion = int.TryParse(quantityText, out quantity);
        (BindingContext as InventoryViewModel).AddToCart(id, quantity);
        Shell.Current.GoToAsync("//Shop");
    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Shop");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryViewModel).RefreshItems();
    }
    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }
}