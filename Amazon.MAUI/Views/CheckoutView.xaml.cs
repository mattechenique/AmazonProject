using Amazon.Library.Models;
using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views;
[QueryProperty(nameof(CartId), "cartId")]
public partial class CheckoutView : ContentPage
{
    public int CartId { get; set; }
    public CheckoutView()
	{
		InitializeComponent();
    }
    private async void BackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//Shop?cartId={CartId}");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CartViewModel(CartId);
        (BindingContext as CartViewModel)?.Checkout();
        (BindingContext as CartViewModel).RefreshItems();
    }
    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }
}