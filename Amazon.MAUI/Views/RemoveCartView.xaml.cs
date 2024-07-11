using Amazon.Library.Models;
using Amazon.MAUI.ViewModels;
namespace Amazon.MAUI.Views;
[QueryProperty(nameof(CartId), "cartId")]
public partial class RemoveCartView : ContentPage
{
    public int CartId { get; set; }
    public RemoveCartView()
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
        (BindingContext as CartViewModel).RemoveFromCart(id, quantity);
        await Shell.Current.GoToAsync($"//Shop?cartId={CartId}");
    }
    private async void CancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//Shop?cartId={CartId}");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CartViewModel(CartId);
        (BindingContext as CartViewModel).RefreshItems();
    }
    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }
}