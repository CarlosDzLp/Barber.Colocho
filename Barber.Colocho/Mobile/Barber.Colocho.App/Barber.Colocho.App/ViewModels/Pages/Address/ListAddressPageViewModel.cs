using Barber.Colocho.App.Db;
using Barber.Colocho.App.Helpers;
using Barber.Colocho.App.Models.Address;
using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Navigation;
using Barber.Colocho.App.ViewModels.Base;
using Barber.Colocho.App.Views.Page.Address;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Barber.Colocho.App.ViewModels.Pages.Address
{
    public class ListAddressPageViewModel : BindableBase
    {
        private readonly IServiceClient serviceClient = new ServiceClient();
        private readonly IDialogService dialogService = new DialogService();

        #region Properties
        public List<AddressModel> ListAddressTemp { get; set; }
        public ObservableCollection<AddressModel> ListAddress { get; set; }
        private AddressModel ItemAddress {  get; set; }
        private string _search;
        public string Search
        {
            get { return _search; }
            set
            {
                if(_search  != value)
                {
                    SetProperty(ref _search, value);
                    OnSearchChange();
                }
            }
        }
        #endregion

        #region Constructor
        public ListAddressPageViewModel()
        {
            AddAddressCommand = new Command(async () => await AddAddressCommandExecuted());
            DeleteAddressCommand = new Command<AddressModel>(async (e) => await DeleteAddressCommandExecuted(e));
            ChangeDefaultAddressCommand = new Command<AddressModel>(async (e) => await ChangeDefaultAddressCommandExecuted(e));
            EditAddressCommand = new Command<AddressModel>(async (e) => await EditAddressCommandExecuted(e));
        }
        #endregion

        #region Command
        public ICommand AddAddressCommand { get; set; }
        public ICommand DeleteAddressCommand { get; set; }
        public ICommand ChangeDefaultAddressCommand { get; set; }
        public ICommand EditAddressCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async Task EditAddressCommandExecuted(AddressModel model)
        {
            try
            {
                if(model != null)
                {
                    await NavigationService.Instance.NavigateToPage(new UpdateAddressPage(model));
                }
            }
            catch(Exception ex)
            {

            }
        }

        private async Task ChangeDefaultAddressCommandExecuted(AddressModel addressModel)
        {
            try
            {
                if (addressModel != null)
                {
                    ItemAddress = addressModel;
                    var e = await dialogService.BottomSheet(Enums.ConfirmPopupEnum.Danger, "¿Desea cambiar la dirección predeterminada?");
                    if (e)
                    {
                        var user = DbContext.Instance.GetToken();
                        var result = await serviceClient.UpdateDefaultAddress(user.UserId, ItemAddress.Id, $"Bearer {user.AccessToken}");
                        if (result != null && result.Result)
                        {
                            await LoadListAddress();
                            dialogService.ModalPopup(Enums.DialogPopupEnum.Success, result.Message);
                        }
                        else
                        {
                            dialogService.ModalPopup(Enums.DialogPopupEnum.Error, "No se pudo eliminar la dirección intente más tarde");
                        }
                    }
                    else
                    {
                        //ListAddress.Where(x => x.Id == ItemAddress.Id).FirstOrDefault().IsDefault = false;
                        ItemAddress = null;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        private async Task DeleteAddressCommandExecuted(AddressModel address)
        {
            try
            {             
                if(address != null)
                {
                    if (!address.IsDefault)
                    {
                        ItemAddress = address;
                        var e = await dialogService.BottomSheet(Enums.ConfirmPopupEnum.Delete, "¿Desea eliminar la dirección?");
                        if (e)
                        {
                            var user = DbContext.Instance.GetToken();
                            var result = await serviceClient.DeleteAddress(user.UserId, ItemAddress.Id, $"Bearer {user.AccessToken}");
                            if (result != null && result.Result)
                            {
                                await LoadListAddress();
                                await dialogService.ModalPopup(Enums.DialogPopupEnum.Success, result.Message);
                            }
                            else
                            {
                               await dialogService.ModalPopup(Enums.DialogPopupEnum.Error,"No se pudo eliminar la dirección intente más tarde");
                            }
                        }
                        else
                            ItemAddress = null;
                    }
                    else
                        await dialogService.ModalPopup(Enums.DialogPopupEnum.Error,"No se puede eliminar una dirección predeterminada");
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
                await NavigationService.Instance.NavigateToPage(new Views.Page.Address.AddAddressPage());
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region Methods
        public async Task LoadListAddress()
        {
            try
            {
                ListAddress = new ObservableCollection<AddressModel>();
                ListAddress.Clear();
                var user = DbContext.Instance.GetToken();
                if (user == null)
                    return;
                var result = await serviceClient.GetListAddressByUserId(user.UserId, $"Bearer {user.AccessToken}");
                if (result != null && result.Result.Count > 0)
                {
                    foreach(var item in result.Result)
                    {
                        item.FontFamily = (item.IsDefault) ? FontAwesome.ProSolid : FontAwesome.ProLight;
                        item.Search = $"{item.Name} {item.ColonyName} {item.CityName} {item.CodePostal} {item.NumExt} {item.Street} {item.StateName}";
                        ListAddress.Add(item);
                    }
                    ListAddressTemp = new List<AddressModel>(ListAddress);
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

        private void OnSearchChange()
        {
            try
            {
                ListAddress?.Clear();
                ListAddress = null;
                if (!string.IsNullOrWhiteSpace(Search))
                {
                    var filter = ListAddressTemp.Where(c => c.Search.Contains(Search)).ToList();
                    ListAddress = new ObservableCollection<AddressModel>(filter);
                }
                else
                {
                    ListAddress = new ObservableCollection<AddressModel>(ListAddressTemp);
                }
            }
            catch(Exception ex) 
            { 
            }
        }
        #endregion
    }
}
