using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views;
[QueryProperty(nameof(CartId), "cartId")]
public partial class ConfigurationView : ContentPage
{
    public int CartId { get; set; }
    public ConfigurationView()
	{
		InitializeComponent();
    }
    private async void OkClicked(object sender, EventArgs e)
    {
        string newTaxRate = TaxRateEntry.Text;
        decimal taxRate;
        bool taxRateConversion = decimal.TryParse(newTaxRate, out taxRate);
        (BindingContext as CartViewModel)?.ChangeTax(taxRate / 100m);
        await Shell.Current.GoToAsync($"//Shop?cartId={CartId}");
    }

    private async void CancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//Shop?cartId={CartId}");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CartViewModel(CartId);
        (BindingContext as CartViewModel).RefreshItems();
    }
}