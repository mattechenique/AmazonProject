using Amazon.MAUI.ViewModels;
using Amazon.Library.Services;
using Amazon.Library.Models;

namespace Amazon.MAUI.Views;
[QueryProperty(nameof(CartId), "cartId")]
public partial class CartView : ContentPage
{
    public int CartId { get; set; }
    public CartView()
	{
		InitializeComponent();
    }
    private async void OkClicked(object sender, EventArgs e)
    {
        string idText = ItemIdEntry.Text;
        string quantityText = ItemQuantityEntry.Text;
        int id, quantity;
        bool idConversion = int.TryParse(idText, out id);
        bool quantityConversion = int.TryParse(quantityText, out quantity);
        (BindingContext as InventoryViewModel).AddToCart(id, quantity);
        await Shell.Current.GoToAsync($"//Shop?cartId={CartId}");
    }
    private async void CancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//Shop?cartId={CartId}");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new InventoryViewModel(CartId);
        (BindingContext as InventoryViewModel).RefreshItems();
    }
    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }
}