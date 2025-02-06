using Barber.Colocho.App.RulesValidations;
using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Message;
using Barber.Colocho.App.Services.Navigation;
using Barber.Colocho.App.Services.Permissions;
using Barber.Colocho.App.ViewModels.Base;
using System.Windows.Input;

namespace Barber.Colocho.App.ViewModels.Session
{
    public class ForgotPasswordPageViewModel: BindableBase
    {
        private readonly IMessageDialog message = new MessageDialog();
        private readonly IDialogService dialogService = new DialogService();
        public IServiceClient serviceClient = new ServiceClient();
        private readonly CheckPermissions checkPermissions = new CheckPermissions();

        #region Properties
        private int UserId { get; set; }
        public bool IsVisibleOne { get; set; } = true;
        public bool IsVisibleTwo { get; set; } = false;
        public string Phone {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string NumberUno { get; set; }
        public string NumberDos { get; set; }
        public string NumberTres { get; set; }
        public string NumberCuatro { get; set; }
        public string NumberCinco {  get; set; }
        #endregion

        #region Constructor
        public ForgotPasswordPageViewModel()
        {
            SendDataCommand = new Command(async () => await SendDataCommandExecuted());
            ForgotPasswordCommand = new Command(async () => await ForgotPasswordCommandExecuted());
            ResendCodeCommand = new Command(async () => await ResendCodeCommandExecuted());
            IsBussy = true;
            _ = OnPermissions();
        }
        #endregion

        #region Command
        public ICommand SendDataCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand ResendCodeCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async Task SendDataCommandExecuted()
        {
            try
            {
                if (!Validations.IsEmail(Email))
                {
                    await message.ToastMessage("El correo no es válido");
                    return;
                }
                if (!Validations.IsPhone(Phone)) 
                {
                    await message.ToastMessage("El teléfono no es válido");
                    return;
                }
                IsBussy = false;
                dialogService.LoadingAsync();
                var result = await serviceClient.SendCodeForgotPassword(new Models.User.SendCodeForgotPasswordModel
                {
                    Phone = Phone,
                    Email = Email
                });
                dialogService.HideAsync();
                IsBussy = true;
                if (result != null && result.Result) 
                {
                    UserId = result.Count;
                    IsVisibleOne = false;
                    IsVisibleTwo = true;
                    if(DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        //se ejecuta el roadcast
                        var receiver = DependencyService.Get<Barber.Colocho.App.Services.Receiver.IBroadcastSender>();
                        if (receiver != null)
                        {
                            string code = await receiver.SendSMS();
                            if (!string.IsNullOrWhiteSpace(code))
                            {
                                NumberUno = code.Substring(0, 1);
                                NumberDos = code.Substring(1, 1);
                                NumberTres = code.Substring(2, 1);
                                NumberCuatro = code.Substring(3, 1);
                                NumberCinco = code.Substring(4, 1);
                            }
                        }
                    }
                }
                else
                {
                    await dialogService.ModalPopup(Enums.DialogPopupEnum.Error, result?.Message);
                }
            }
            catch(Exception ex)
            {
                IsBussy = true;
                dialogService.HideAsync();
            }
        }

        private async Task ResendCodeCommandExecuted()
        {
            try
            {
                IsVisibleOne = true;
                IsVisibleTwo = false;
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    //se ejecuta el roadcast
                    var receiver = DependencyService.Get<Barber.Colocho.App.Services.Receiver.IBroadcastSender>();
                    receiver.UnReceiver();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async Task ForgotPasswordCommandExecuted()
        {
            try
            {
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    //se ejecuta el roadcast
                    var receiver = DependencyService.Get<Barber.Colocho.App.Services.Receiver.IBroadcastSender>();
                    receiver.UnReceiver();
                }

                if (string.IsNullOrWhiteSpace(NumberUno)
                    || string.IsNullOrWhiteSpace(NumberDos)
                    || string.IsNullOrWhiteSpace(NumberTres)
                    || string.IsNullOrWhiteSpace(NumberCuatro)
                    | string.IsNullOrWhiteSpace(NumberCinco))
                {
                    await message.SnackBar("Hay datos incompletos", SnackBarType.Error);
                    return;
                }

                if (!Validations.IsPassword(Password))
                {
                    await message.ToastMessage("La contraseña no es válida");
                    return;
                }
                string codeString = $"{NumberUno}{NumberDos}{NumberTres}{NumberCuatro}{NumberCinco}";
                IsBussy = false;
                dialogService.LoadingAsync();
                var result = await serviceClient.ForgotPassword(new Models.User.ForgotPasswordModel
                {
                    UserId = UserId,
                    Password = Password,
                    Code = codeString
                });
                dialogService.HideAsync();
                IsBussy = true;
                if(result != null && result.Result)
                {
                    await dialogService.ModalPopup(Enums.DialogPopupEnum.Success,result?.Message);
                    await NavigationService.Instance.NavigationRoot();
                }
                else
                {
                    await dialogService.ModalPopup(Enums.DialogPopupEnum.Error, result?.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                IsBussy = true;
                dialogService.HideAsync();
            }
        }
        #endregion

        #region Methods
        private async Task OnPermissions()
        {
            try
            {
                if(DeviceInfo.Platform == DevicePlatform.Android)
                {
                    var status = await checkPermissions.CheckAndRequestPermissionAsync(new Permissions.Sms(), "Necesitamos el permiso para poder leer los mensajes, y ahi facilitar el llenado de los datos", "sms_permission_icon.png");
                    if (status == PermissionStatus.Granted)
                    {
                        IsBussy = true;
                    }
                    else
                    {
                        IsBussy = false;
                    }
                }
                
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
