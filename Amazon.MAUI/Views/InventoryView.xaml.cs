using Amazon.Library.Services;
using Amazon.MAUI.ViewModels;
namespace Amazon.MAUI.Views;

public partial class InventoryView : ContentPage
{
    public InventoryView()
    {
        InitializeComponent();
        BindingContext = new InventoryViewModel();
    }

    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel).UpdateItem();
    }
    private void CreateClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ItemView");
    }
    private void ListClicked(object sender, EventArgs e)
    {


    }
    private void UpdateClicked(object sender, EventArgs e)
    {

    }
    private void InlineDeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel).DeleteItem();
        //(BindingContext as InventoryViewModel).RefreshItems();
    }
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");

    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryViewModel).RefreshItems();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }

    private void InlineDelete_Clicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel).RefreshItems();
    }
}