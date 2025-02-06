using Barber.Colocho.App.Db;
using Barber.Colocho.App.RulesValidations;
using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Navigation;
using Barber.Colocho.App.ViewModels.Base;
using Barber.Colocho.App.Views.Session;
using System.Windows.Input;

namespace Barber.Colocho.App.ViewModels.Session
{
    public class LoginPageViewModel : BindableBase
    {
        private readonly IServiceClient serviceClient = new ServiceClient();
        private readonly IDialogService dialogService = new DialogService();

        #region Properties
        public string Phone {  get; set; }
        public string Password { get; set; }
        #endregion

        #region Constructor
        public LoginPageViewModel()
        {
#if DEBUG
            Phone = "9616505473";
            Password = "Carlosdiaz90#";
#endif

            IsBussy = true;
            LoginCommand = new Command(async () => await LoginCommandExecuted());
            TapCommand = new Command(async () => await NavigationService.Instance.NavigateToPage(new RegisterPage()));
            ForgotPasswordCommand = new Command(async () => await NavigationService.Instance.NavigateToPage(new ForgotPasswordPage()));
        }
        #endregion

        #region Command
        public ICommand LoginCommand { get; set; }
        public ICommand TapCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async Task LoginCommandExecuted()
        {
            try
            {
                if (!Validations.IsPhone(Phone))
                {
                    return;
                }
                if (!Validations.IsPassword(Password))
                {
                    return;
                }
                IsBussy = false;
                dialogService.LoadingAsync();
                var result = await serviceClient.Authenticate(new Models.Authenticate.AuthenticateModel
                {
                    Phone = Phone,
                    Password = Password,
                });
                dialogService.HideAsync();
                IsBussy = true;
                if (result != null && result.Result != null)
                {
                    DbContext.Instance.InsertToken(result.Result);
                    await NavigationService.Instance.OnPrincipal();
                }
                else
                    await dialogService.ModalPopup(Enums.DialogPopupEnum.Error, result?.Message);
            }
            catch (Exception ex)
            {
                IsBussy = true;
                dialogService.HideAsync();
            }
        }
        #endregion
    }
}
