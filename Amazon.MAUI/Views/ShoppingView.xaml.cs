using Amazon.MAUI.ViewModels;
using System;
using Microsoft.Maui.Controls;
using Amazon.Library.Models;
namespace Amazon.MAUI.Views;
[QueryProperty(nameof(CartId), "cartId")]
public partial class ShoppingView : ContentPage
{
    public int CartId { get; set; }
    public ShoppingView()
    {
        InitializeComponent();
    }

    private async void AddItemClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//CartView?cartId={CartId}");
    }

    private async void CheckoutClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//CheckoutView?cartId={CartId}");
    }
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//CartListView");

    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CartViewModel(CartId);
        (BindingContext as CartViewModel).RefreshItems();
    }
    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }

    private async void RemoveItemClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//RemoveCartView?cartId={CartId}");
    }

    private async void ConfigurationClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//ConfigurationView?cartId={CartId}");
    }
    private void InlineChangeBogoClicked(object sender, EventArgs e)
    {
        (BindingContext as CartViewModel).ChangeBogo();
    }
    private void InlineChangeBogo_Clicked(object sender, EventArgs e)
    {
        (BindingContext as CartViewModel).RefreshItems();
    }
    private void InlineChangeMarkdown_Clicked(object sender, EventArgs e)
    {
        (BindingContext as CartViewModel).RefreshItems();
    }
}