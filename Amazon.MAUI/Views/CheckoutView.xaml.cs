using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views;

public partial class CheckoutView : ContentPage
{
	public CheckoutView()
	{
		InitializeComponent();
        BindingContext = new CartViewModel();
    }

    private void BackClicked(object sender, EventArgs e)
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