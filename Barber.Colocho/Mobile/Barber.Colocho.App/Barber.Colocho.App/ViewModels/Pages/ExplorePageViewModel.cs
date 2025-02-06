using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Message;
using Barber.Colocho.App.Services.Navigation;
using Barber.Colocho.App.Services.Permissions;
using Barber.Colocho.App.ViewModels.Base;
using System.Windows.Input;

namespace Barber.Colocho.App.ViewModels.Pages
{
    public class ExplorePageViewModel : BindableBase
    {
        private readonly IServiceClient serviceClient = new ServiceClient();
        private readonly IDialogService dialogService = new DialogService();
        //private readonly IMessage message = new Message();
        private readonly CheckPermissions checkPermissions = new CheckPermissions();


        #region Properties
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Location { get; set; }
        #endregion

        #region Constructor
        public ExplorePageViewModel()
        {
            FilterCommand = new Command(async () => await FilterCommandExecuted());
        }
        #endregion

        #region Command
        public ICommand FilterCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async Task FilterCommandExecuted()
        {
            try
            {
                var result = await dialogService.FilterMap(Latitude, Longitude);
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region Methods
        public async Task LoadPosition(double latitude, double longitude)
        {
            try
            {
                Location = string.Empty;
                Latitude = latitude;
                Longitude = longitude;
                IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);
                Placemark placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    Location = $"{placemark.Thoroughfare} #{placemark.SubThoroughfare} {placemark.SubLocality} {placemark.PostalCode}, {placemark.Locality}, {placemark.AdminArea}";
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
