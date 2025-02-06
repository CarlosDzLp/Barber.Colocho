using Barber.Colocho.App.Services.GoogleMpas;
using Barber.Colocho.App.ViewModels.Pages;
using Maui.GoogleMaps;
using System.Timers;

namespace Barber.Colocho.App.Views.Page;

public partial class ExplorePage : ContentPage
{
    private double latitude = 0, longitude = 0;
    private System.Timers.Timer _timer;
    ExplorePageViewModel explorePageViewModel;
    public ExplorePage()
	{
		InitializeComponent();
        mapExplore.MapStyle = StyleMap.Style();
        _ = Location();
        this.BindingContext = explorePageViewModel = new ExplorePageViewModel();
    }

    

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        try
        {
            Console.WriteLine($"Timer position");
            var position = mapExplore.CameraPosition.Target;
            if (position.Latitude != latitude && position.Longitude != longitude)
            {
                MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    latitude = position.Latitude;
                    longitude = position.Longitude;
                    mapExplore.Circles.Clear();
                    var circle = new Circle();
                    circle.StrokeWidth = 2;
                    circle.StrokeColor = Color.FromRgba(218, 39, 77, 120);
                    circle.FillColor = Color.FromRgba(218, 39, 77, 120);
                    circle.Center = new Position(latitude, longitude);
                    circle.Radius = Distance.FromMeters(500);
                    circle.ZIndex = 100;
                    mapExplore.Circles.Add(circle);
                    mapExplore.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude), Distance.FromMeters(600)), true);
                    await explorePageViewModel.LoadPosition(latitude, longitude);
                    Console.WriteLine($"Nueva posicion: Latitude:{latitude} , Longitude:{longitude}");
                });
            }
        }
        catch(Exception ex)
        {

        }
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
                mapExplore.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(600)), true);
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        OnTimer();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        StopTimer();
    }

    private void OnTimer()
    {
        try
        {
            if (_timer == null)
            {
                _timer = new System.Timers.Timer(500);
                _timer.Elapsed += OnTimerElapsed;
                _timer.Start();
            }
            else
            {
                _timer.Elapsed -= OnTimerElapsed;
                _timer.Stop();
                _timer = null;
                OnTimer();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void StopTimer()
    {
        if(_timer != null)
        {
            _timer.Elapsed -= OnTimerElapsed;
            _timer.Stop();
        }
        _timer = null;
    }
}