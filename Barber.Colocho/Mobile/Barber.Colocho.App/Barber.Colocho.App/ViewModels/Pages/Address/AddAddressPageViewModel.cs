using Barber.Colocho.App.Db;
using Barber.Colocho.App.Models.Base;
using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Message;
using Barber.Colocho.App.Services.Navigation;
using Barber.Colocho.App.Services.Permissions;
using Barber.Colocho.App.ViewModels.Base;
using Maui.GoogleMaps;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Barber.Colocho.App.ViewModels.Pages.Address
{
    public class AddAddressPageViewModel : BindableBase
    {
        private readonly IDialogService loadingService = new DialogService();
        private readonly IServiceClient serviceClient = new ServiceClient();
        private readonly CheckPermissions checkPermissions = new CheckPermissions();
        private readonly IMessageDialog message = new MessageDialog();

        #region Properties
        public string Name { get; set; }
        public string Street { get; set; }
        public string NumExt { get; set; }
        public int IdColony { get; set; }
        public string CodePostal { get; set; }
        public string Observations { get; set; }
        public bool IsDefault { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public ObservableCollection<GenericModel> ListColony { get; set; }
        public ObservableCollection<Pin> Pins { get; set; }
        #endregion

        #region Constructor
        public AddAddressPageViewModel()
        {
            AddAddressCommand = new Command(async () => await AddAddressCommandExecuted());
            ChangeLocationCommand = new Command(async () => await ChangeLocationCommandExecuted());
            _ = LoadUbication();
        }
        #endregion

        #region Command
        public ICommand AddAddressCommand { get; set; }
        public ICommand ChangeLocationCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async Task ChangeLocationCommandExecuted()
        {
            try
            {
                var result = await loadingService.LocationPopup();
                if (result != null)
                {
                    Latitude = result.Value.Latitude;
                    Longitude = result.Value.Longitude;
                    if (Pins != null && Pins.Count > 0)
                    {
                        Pins.Clear();
                    }
                    Pins = new ObservableCollection<Pin>()
                    {
                        new Pin() {Position = new Position(Latitude, Longitude) },
                    };
                    await ReverseGeocoding();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private async Task AddAddressCommandExecuted()
        {
            try
            {
                var resultPermission = await LoadPermission();
                if (!resultPermission)
                {
                    message.ToastMessage("No tiene permiso para localicación");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Name))
                {
                    message.ToastMessage("Agregue el nombre de la dirección");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Street))
                {
                    message.ToastMessage("Agregue la calle");
                    return;
                }

                if (string.IsNullOrWhiteSpace(NumExt))
                {
                    message.ToastMessage("Agregue el número exterior");
                    return;
                }
                if (string.IsNullOrWhiteSpace(CodePostal))
                {
                    message.ToastMessage("Agregue el código postal");
                    return;
                }
                if(IdColony == 0)
                {
                    message.ToastMessage("Seleccione una colonia");
                    return;
                }
                if (string.IsNullOrWhiteSpace(Observations))
                {
                    message.ToastMessage("Agregue una observación");
                    return;
                }

                var e = await loadingService.BottomSheet(Enums.ConfirmPopupEnum.Danger, "¿Desea guardar la dirección?");
                if (e)
                {
                    var user = DbContext.Instance.GetToken();
                    loadingService.LoadingAsync();
                    var result = await serviceClient.AddAddressUser(user.UserId, new Models.Address.AddAdressModel
                    {
                        CodePostal = CodePostal,
                        IdColony = IdColony,
                        IsDefault = IsDefault,
                        Latitude = Latitude,
                        Longitude = Longitude,
                        Name = Name,
                        NumExt = NumExt,
                        Observations = Observations,
                        Street = Street
                    }, $"Bearer {user.AccessToken}");
                    loadingService.HideAsync();
                    if (result != null && result.Result)
                    {
                        await loadingService.ModalPopup(Enums.DialogPopupEnum.Success, result?.Message);
                        await NavigationService.Instance.GoBack();
                    }
                    else
                    {
                        await loadingService.ModalPopup(Enums.DialogPopupEnum.Error,result?.Message);
                    }
                }
            }
            catch(Exception ex)
            {
                loadingService.HideAsync();
            }
        }


        #endregion

        #region Methods
        private async Task<bool> LoadPermission()
        {
            try
            {
                var result = await checkPermissions.CheckAndRequestPermissionAsync(new Permissions.LocationWhenInUse(), "Necesitamos el permiso, para poder localiza tu domicilio", "permission_location.svg");
                if(result == PermissionStatus.Granted)
                {                   
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private async Task LoadUbication()
        {
            try
            {
                var result = await LoadPermission();
                if (result)
                {
                    GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                    var _cancelTokenSource = new CancellationTokenSource();

                    Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                    if (location != null)
                    {
                        Latitude = location.Latitude;
                        Longitude = location.Longitude;
                    }
                    Pins = new ObservableCollection<Pin>()
                    {
                        new Pin() {Position = new Position(Latitude, Longitude) },
                    };
                    await ReverseGeocoding();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public async Task SearchCodePostal(string text)
        {
            try
            {
                ListColony = new ObservableCollection<GenericModel>();
                IdColony = 0;
                loadingService.LoadingAsync();
                var result = await serviceClient.GetCodePostal(text);
                loadingService.HideAsync();
                if(result != null && result.Result != null && result.Result.City != null && result.Result.State != null && result.Result.ListColony != null && result.Result.ListColony.Count() > 0)
                {
                    foreach (var item in result.Result.ListColony) 
                    {
                        ListColony.Add(item);
                    }
                    State = result.Result.State.Name;
                    City = result.Result.City.Name;
                }
            }
            catch(Exception ex)
            {
                loadingService.HideAsync();
            }
        }
        public async Task ReverseGeocoding()
        {
            try
            {
                IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(Latitude, Longitude);
                Placemark placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    Street = placemark.Thoroughfare;
                    CodePostal = placemark.PostalCode;
                    await SearchCodePostal(CodePostal);
                }
            }
            catch (Exception ex) 
            {
            }
        }

        public bool CloseBottomSheet()
        {
            try
            {
                MopupService.Instance.PopAllAsync(true);
                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        #endregion
    }
}
