using Amazon.MAUI.ViewModels;
namespace Amazon.MAUI.Views;

public partial class ShoppingView : ContentPage
{
    public ShoppingView()
    {
        InitializeComponent();
        BindingContext = new CartViewModel();
    }

    private void AddItemClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//CartView");
    }

    private async void CheckoutClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//CheckoutView");
    }
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");

    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as CartViewModel).RefreshItems();
    }
    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }

    private void RemoveItemClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//RemoveCartView");
    }
}