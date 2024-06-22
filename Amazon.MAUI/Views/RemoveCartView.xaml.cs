using Amazon.MAUI.ViewModels;
namespace Amazon.MAUI.Views;

public partial class RemoveCartView : ContentPage
{
	public RemoveCartView()
	{
		InitializeComponent();
        BindingContext = new CartViewModel();
	}
    private void OkClicked(object sender, EventArgs e)
    {
        string idText = ItemIdEntry.Text;
        string quantityText = ItemQuantityEntry.Text;
        int id, quantity;
        bool idConversion = int.TryParse(idText, out id);
        bool quantityConversion = int.TryParse(quantityText, out quantity);
        (BindingContext as CartViewModel).RemoveFromCart(id, quantity);
        Shell.Current.GoToAsync("//Shop");
    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Shop");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as CartViewModel).RefreshItems();
    }
    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }
}