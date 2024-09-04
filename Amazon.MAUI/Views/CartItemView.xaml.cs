using Amazon.Library.Models;
using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views;
[QueryProperty(nameof(ItemId), "itemId")]
[QueryProperty(nameof(CartId), "cartId")]
public partial class CartItemView : ContentPage
{
    public int ItemId { get; set; }
    public int CartId { get; set; }
    public CartItemView()
	{
		InitializeComponent();
	}
    private async void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as CartItemViewModel).ChangeMarkdown();
        await Shell.Current.GoToAsync($"//Shop?cartId={CartId}");
    }

    private async void CancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//Shop?cartId={CartId}");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CartItemViewModel(CartId, ItemId);
    }
}