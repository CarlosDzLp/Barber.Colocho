using Barber.Colocho.App.Models.Filter;

namespace Barber.Colocho.App.Views.Page;

public partial class FilterPage : ContentPage
{
	public event EventHandler<FilterModel> FilterChanged;
	public FilterPage(double latitude, double longitude)
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		try
		{
			App.Current.MainPage.Navigation.PopModalAsync(true);
			FilterChanged?.Invoke(this, null);
		}
		catch(Exception ex)
		{

		}
    }

    protected override bool OnBackButtonPressed()
    {
		return true;
    }
}