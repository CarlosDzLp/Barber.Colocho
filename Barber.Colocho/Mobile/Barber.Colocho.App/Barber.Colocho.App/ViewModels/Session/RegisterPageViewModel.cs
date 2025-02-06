using Barber.Colocho.App.Models.User;
using Barber.Colocho.App.RulesValidations;
using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Message;
using Barber.Colocho.App.Services.Navigation;
using Barber.Colocho.App.Services.Permissions;
using Barber.Colocho.App.ViewModels.Base;
using Barber.Colocho.App.Views.Session;
using System.Windows.Input;

namespace Barber.Colocho.App.ViewModels.Session
{
    public class RegisterPageViewModel : BindableBase
    {
        private readonly IServiceClient serviceClient = new ServiceClient();
        private readonly IDialogService dialogService = new DialogService();
        private readonly IMessageDialog message = new MessageDialog();
        private readonly CheckPermissions checkPermissions = new CheckPermissions();

        #region Properties
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Color IsEnabledColorButton { get; set; } = Color.Parse("#E6EAEE");
        #endregion

        #region Constructor
        public RegisterPageViewModel()
        {
#if DEBUG
            Email = "ryankar90@hotmail.com";
            Name = "Carlos";
            LastName = "Diaz Lopez";
            Phone = "9616505473";
            Password = "Carlosdiaz90#";
#endif

            IsBussy = true;
            RegisterCommand = new Command(async () => await RegisterCommandExecuted());
            TapCommand = new Command(async () => await NavigationService.Instance.NavigateToPage(new LoginPage()));
            _ = OnPermissions();
        }
        #endregion

        #region Methods
        private async Task OnPermissions()
        {
            try
            {
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    var status = await checkPermissions.CheckAndRequestPermissionAsync(new Permissions.Sms(), "Necesitamos el permiso para poder leer los mensajes, y ahi facilitar el llenado de los datos", "sms_permission_icon.png");
                    if (status == PermissionStatus.Granted)
                    {
                        IsBussy = true;
                        IsEnabledColorButton = Color.Parse("#000000");
                    }
                    else
                    {
                        IsBussy = false;
                        IsEnabledColorButton = Color.Parse("#E6EAEE");
                    }
                }
                    
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region Command
        public ICommand RegisterCommand { get; set; }
        public ICommand TapCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async Task RegisterCommandExecuted()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    await message.ToastMessage("Debe agregar el nombre");
                    return;
                }
                if (string.IsNullOrWhiteSpace(LastName)) 
                {
                    await message.ToastMessage("Debe agregar los apellidos");
                    return;
                }
                if(!Validations.IsEmail(Email))
                {
                    await message.ToastMessage("El correo no es valido");
                    return;
                }
                if (!Validations.IsPhone(Phone)) 
                {
                    await message.ToastMessage($"El teléfono no es válido");
                    return;
                }
                if (!Validations.IsPassword(Password))
                {
                    await message.ToastMessage("La contraseña no es valida");
                    return;
                }
                var dta = new AddUserModel
                {
                    Email = Email,
                    Phone = Phone,
                    Name = Name,
                    LastName = LastName,
                    Password = Password
                };
                await NavigationService.Instance.NavigateToPage(new Views.Session.VerificationCodePage(dta));
            }
            catch(Exception ex)
            {
                IsBussy = true;
                dialogService.HideAsync();
            }
        }
        #endregion
    }
}
