using Barber.Colocho.App.Db;
using Barber.Colocho.App.Models.Address;
using Barber.Colocho.App.RulesValidations;
using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Message;
using Barber.Colocho.App.Services.Navigation;
using Barber.Colocho.App.ViewModels.Base;
using System.Windows.Input;

namespace Barber.Colocho.App.ViewModels.Pages.Company
{
    public class AddCompanyPageViewModel : BindableBase
    {
        IDialogService dialogService = new DialogService();
        IServiceClient serviceClient = new ServiceClient();
        IMessageDialog message = new MessageDialog();

        #region Properties
        public string Name { get; set; }
        public string RFC {  get; set; }
        public string Description { get; set; }
        public AddressModel Address { get; set; }
        public bool IsVisibleAddress { get; set; }
        #endregion

        #region Constructor
        public AddCompanyPageViewModel()
        {
            AddCompanyCommand = new Command(async () => await AddCompanyCommandExecuted());
            DeleteAddressCommand = new Command(async () => await DeleteAddressCommandExecuted());
        }
        #endregion

        #region Command
        public ICommand AddCompanyCommand { get; set; }
        public ICommand DeleteAddressCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async Task DeleteAddressCommandExecuted()
        {
            try
            {
                Address = null;
                IsVisibleAddress = false;
            }
            catch(Exception ex)
            {

            }
        }
        private async Task AddCompanyCommandExecuted()
        {
            try
            {
                if(string.IsNullOrWhiteSpace(Name))
                {
                    message.ToastMessage("Ingrese un nombre");
                    return;
                }
                if (!string.IsNullOrWhiteSpace(RFC)) 
                {
                    if (!Validations.IsRFC(RFC))
                    {
                        message.ToastMessage("Ingrese un RFC válido");
                        return;
                    }
                }
                if(string.IsNullOrWhiteSpace(Description))
                {
                    message.ToastMessage("Agregue una descrpción");
                    return;
                }
                
                if(Address == null)
                {
                    var addresResult = await dialogService.AddressPopup();
                    Address = addresResult;
                    IsVisibleAddress = (Address != null) ? true : false;
                    return;
                }
                var result = await dialogService.BottomSheet(Enums.ConfirmPopupEnum.Save, $"¿Desea crear la barbería {Name}?");
                if (result)
                {
                    var user = DbContext.Instance.GetToken();
                    dialogService.LoadingAsync();
                    var response = await serviceClient.AddCompany(user.UserId, new Models.Company.AddCompanyModel
                    {
                        AddressId = Address.Id,
                        Description = Description,
                        Name = Name,
                        Rfc = RFC,
                    }, $"Bearer {user.AccessToken}");
                    dialogService.HideAsync();
                    if (response != null && response.Result)
                    {
                        await dialogService.ModalPopup(Enums.DialogPopupEnum.Success, response?.Message);
                        await NavigationService.Instance.GoBack();
                    }
                    else
                        await dialogService.ModalPopup(Enums.DialogPopupEnum.Error, response?.Message);
                }
            }
            catch(Exception ex)
            {
                dialogService.HideAsync();
            }
        }
        #endregion

        #region Methods

        #endregion
    }
}
