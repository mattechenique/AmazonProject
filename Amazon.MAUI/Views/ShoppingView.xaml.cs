using Amazon.MAUI.ViewModels;
namespace Amazon.MAUI.Views;

public partial class ShoppingView : ContentPage
{
    public ShoppingView()
    {
        InitializeComponent();
    }

    private void AddItemClicked(object sender, EventArgs e)
    {

    }

    private void CheckoutClicked(object sender, EventArgs e)
    {


    }
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");

    }
}