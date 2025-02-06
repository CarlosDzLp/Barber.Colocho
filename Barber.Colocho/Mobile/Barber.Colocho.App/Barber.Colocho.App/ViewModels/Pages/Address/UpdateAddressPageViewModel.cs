using Barber.Colocho.App.Db;
using Barber.Colocho.App.Models.Address;
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
    public class UpdateAddressPageViewModel : BindableBase
    {
        private readonly IServiceClient serviceClient = new ServiceClient();
        private readonly IDialogService dialogService = new DialogService();
        private readonly IMessageDialog message = new MessageDialog();
        private readonly CheckPermissions checkPermissions = new CheckPermissions();


        #region Properties
        public AddressModel Address { get; set; }
        public ObservableCollection<Pin> Pins { get; set; }
        public ObservableCollection<GenericModel> ListColony { get; set; }
        public int IdColony { get; set; }
        public GenericModel ColonySelected { get; set; }
        #endregion

        #region Constructor
        public UpdateAddressPageViewModel(AddressModel parameter)
        {
            if (parameter != null)
            {
                Address = parameter;
                _ = OnLoadData();
            }
            ChangeLocationCommand = new Command(async () => await ChangeLocationCommandExecuted());
            UpdateAddressCommand = new Command(async () => await UpdateAddressCommandExecuted());
        }
        #endregion

        #region Command
        public ICommand ChangeLocationCommand { get; set; }
        public ICommand UpdateAddressCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async Task UpdateAddressCommandExecuted()
        {
            try
            {
                var resultPermission = await LoadPermission();
                if (!resultPermission)
                {
                    message.ToastMessage("No tiene permiso para localicación");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Address.Name))
                {
                    message.ToastMessage("Agregue el nombre de la dirección");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Address.Street))
                {
                    message.ToastMessage("Agregue la calle");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Address.NumExt))
                {
                    message.ToastMessage("Agregue el número exterior");
                    return;
                }
                if (string.IsNullOrWhiteSpace(Address.CodePostal))
                {
                    message.ToastMessage("Agregue el código postal");
                    return;
                }
                if (IdColony == 0)
                {
                    message.ToastMessage("Seleccione una colonia");
                    return;
                }
                if (string.IsNullOrWhiteSpace(Address.Observations))
                {
                    message.ToastMessage("Agregue una observación");
                    return;
                }
                var e = await dialogService.BottomSheet(Enums.ConfirmPopupEnum.Danger, "¿Desea actualizar la dirección?");
                if (e)
                {
                    var user = DbContext.Instance.GetToken();
                    dialogService.LoadingAsync();
                    var result = await serviceClient.UpdateAddress(user.UserId, Address.Id, new Models.Address.AddAdressModel
                    {
                        CodePostal = Address.CodePostal,
                        IdColony = IdColony,
                        Observations = Address.Observations,
                        IsDefault = Address.IsDefault,
                        Latitude = Address.Latitude,
                        Longitude = Address.Longitude,
                        Name = Address.Name,
                        NumExt = Address.NumExt,
                        Street = Address.Street,
                    }, $"Bearer {user.AccessToken}");
                    dialogService.HideAsync();
                    if (result != null && result.Result)
                    {
                        await dialogService.ModalPopup(Enums.DialogPopupEnum.Success, result?.Message);
                        await NavigationService.Instance.GoBack();
                    }
                    else
                    {
                        await dialogService.ModalPopup(Enums.DialogPopupEnum.Error, result?.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                dialogService.HideAsync();
            }
        }
        private async Task ChangeLocationCommandExecuted()
        {
            try
            {
                var e = await dialogService.LocationPopup();
                if (e != null)
                {
                    Address.Latitude = e.Value.Latitude;
                    Address.Longitude = e.Value.Longitude;
                    if (Pins != null && Pins.Count > 0)
                    {
                        Pins.Clear();
                    }
                    Pins = new ObservableCollection<Pin>()
                    {
                        new Pin() {Position = new Position(Address.Latitude, Address.Longitude) },
                    };
                    await ReverseGeocoding();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Methods
        private async Task OnLoadData()
        {
            try
            {
                await LoadPermission();
                var user = DbContext.Instance.GetToken();
                if(Address != null)
                {
                    dialogService.LoadingAsync();
                    var result = await serviceClient.GetAddressById(user.UserId, Address.Id, $"Bearer {user.AccessToken}");
                    dialogService.HideAsync();
                    if (result != null && result.Result != null)
                    {
                        Address = result.Result;
                        
                        if (Pins != null && Pins.Count > 0)
                        {
                            Pins.Clear();
                        }
                        Pins = new ObservableCollection<Pin>()
                        {
                            new Pin() {Position = new Position(Address.Latitude, Address.Longitude) },
                        };
                        await SearchCodePostal(Address.CodePostal);
                        if(ListColony != null && ListColony.Count > 0)
                        {
                            var filter = ListColony.Where(c => c.Id == Address.IdColony).FirstOrDefault();
                            if(filter != null)
                            {
                                ColonySelected = filter;
                                IdColony = filter.Id;
                            }                         
                        }
                    }
                    else
                        Address = null;
                }
            }
            catch(Exception ex)
            {
                dialogService.HideAsync();
            }
        }

        public async Task SearchCodePostal(string text)
        {
            try
            {
                ListColony = new ObservableCollection<GenericModel>();
                dialogService.LoadingAsync();
                var result = await serviceClient.GetCodePostal(text);
                dialogService.HideAsync();
                IdColony = 0;
                if (result != null && result.Result != null && result.Result.City != null && result.Result.State != null && result.Result.ListColony != null && result.Result.ListColony.Count() > 0)
                {
                    foreach (var item in result.Result.ListColony)
                    {
                        ListColony.Add(item);
                    }
                    string State = result.Result.State.Name;
                    string City = result.Result.City.Name;
                    Address.CityName = City;
                    Address.StateName = State;
                }
            }
            catch (Exception ex)
            {
                dialogService.HideAsync();
            }
        }

        private async Task<bool> LoadPermission()
        {
            try
            {
                var result = await checkPermissions.CheckAndRequestPermissionAsync(new Permissions.LocationWhenInUse(), "Necesitamos el permiso, para poder localiza tu domicilio", "permission_location.svg");
                if (result == PermissionStatus.Granted)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task ReverseGeocoding()
        {
            try
            {
                IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(Address.Latitude, Address.Longitude);
                Placemark placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    Address.Street = placemark.Thoroughfare;
                    Address.CodePostal = placemark.PostalCode;
                    await SearchCodePostal(Address.CodePostal);
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
