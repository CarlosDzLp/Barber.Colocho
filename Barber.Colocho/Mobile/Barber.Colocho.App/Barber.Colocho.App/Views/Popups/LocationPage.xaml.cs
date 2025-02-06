using Barber.Colocho.App.Services.GoogleMpas;
using Maui.GoogleMaps;

namespace Barber.Colocho.App.Views.Popups;

public partial class LocationPage : ContentPage
{
	public event EventHandler<Maui.GoogleMaps.Position?> LocationChanged;
    private Maui.GoogleMaps.Position Position { get; set; }
    public LocationPage()
	{
		InitializeComponent();
		mapLocation.MapStyle = StyleMap.Style();
		_ = Location();
    }

	private async Task Location()
	{
		try
		{
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var _cancelTokenSource = new CancellationTokenSource();
            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
            if (location != null)
            {
                mapLocation.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(700)), true);
                Address(location.Latitude, location.Longitude);
            }
        }
		catch(Exception ex)
		{

		}
	}

    protected override bool OnBackButtonPressed()
    {
		LocationChanged?.Invoke(this, null);
        return base.OnBackButtonPressed();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        LocationChanged?.Invoke(this, null);
        App.Current.MainPage.Navigation.RemovePage(this);
    }

    private async void mapLocation_CameraIdled(object sender, CameraIdledEventArgs e)
    {
        try
        {
            var posi = e.Position.Target;
            if (posi != null)
            {
                Position = posi;
                Address(Position.Latitude, Position.Longitude);              
            }              
        }
        catch(Exception ex)
        {

        }
    }

    private void Address(double latitude,double longitude)
    {
        try
        {
            MainThread.InvokeOnMainThreadAsync(async () => 
            {
                IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);
                Placemark placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    lbltitle.Text = $"{placemark.Thoroughfare} #{placemark.SubThoroughfare} {placemark.SubLocality} {placemark.PostalCode}, {placemark.Locality}, {placemark.AdminArea}";
                }
            });            
        }
        catch(Exception ex)
        {

        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            LocationChanged?.Invoke(this, Position);
            App.Current.MainPage.Navigation.RemovePage(this);
        }
        catch(Exception ex)
        {

        }
    }
}