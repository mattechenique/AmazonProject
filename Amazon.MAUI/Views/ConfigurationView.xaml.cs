using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views;

public partial class ConfigurationView : ContentPage
{
	public ConfigurationView()
	{
		InitializeComponent();
        BindingContext = new CartViewModel();
    }
    private void OkClicked(object sender, EventArgs e)
    {
        if (decimal.TryParse(TaxRateEntry.Text, out decimal newTaxRate))
        {
            (BindingContext as CartViewModel)?.ChangeTax(newTaxRate / 100m);
        }
        Shell.Current.GoToAsync("//Shop");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Shop");
    }
}