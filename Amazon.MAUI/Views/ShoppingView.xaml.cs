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
        BindingContext = new CartViewModel(CartId);
    }

    private void AddItemClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//CartView");
    }

    private async void CheckoutClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//CheckoutView");
    }
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//CartListView");

    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as CartViewModel).RefreshItems();
        BindingContext = new CartViewModel(CartId);
    }
    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }

    private void RemoveItemClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//RemoveCartView");
    }

    private void ConfigurationClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ConfigurationView");
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