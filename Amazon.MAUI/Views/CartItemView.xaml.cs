using Amazon.Library.Models;
using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views;
[QueryProperty(nameof(ItemId), "itemId")]
[QueryProperty(nameof(CartItemId), "cartitemId")]
public partial class CartItemView : ContentPage
{
    public int ItemId { get; set; }
    public int CartItemId { get; set; }
    public CartItemView()
	{
		InitializeComponent();
	}
    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as CartItemViewModel).ChangeMarkdown();
        Shell.Current.GoToAsync("//Shop");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Shop");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CartItemViewModel(CartItemId, ItemId);
    }
}