using Barber.Colocho.App.Models.User;
using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Message;
using Barber.Colocho.App.Services.Navigation;
using Barber.Colocho.App.ViewModels.Base;
using System.Reflection.Metadata;
using System.Windows.Input;

namespace Barber.Colocho.App.ViewModels.Session
{
    public class VerificationCodePageViewModel : BindableBase
    {
        private readonly IDialogService dialogService = new DialogService();
        private readonly IMessageDialog message = new MessageDialog();
        private readonly IServiceClient serviceClient = new ServiceClient();

        #region Properties
        public int UserId { get; set; }
        public string Time {  get; set; }
        public bool IsEnabledResendCode {  get; set; }
        public Color IsEnabledColor { get; set; }
        public Color IsEnabledButton { get; set; }
        public string NumberUno { get; set; }
        public string NumberDos { get; set; }
        public string NumberTres { get; set; }
        public string NumberCuatro { get; set; }
        private string _numberCinco;
        public string NumberCinco 
        {
            get => _numberCinco;
            set 
            {
                if (_numberCinco != value) 
                {
                    SetProperty(ref _numberCinco, value);
                    _ = OnValidateNumber();
                }
            }
        }
        public bool IsEnabledEntry { get; set; }
        #endregion

        #region Constructor
        public VerificationCodePageViewModel(AddUserModel parameter)
        {
            _ = OnRegister(parameter);
            SendCodeCommand = new Command(async () => await SendCodeCommandExecuted());
        }
        #endregion

        #region Command
        public ICommand SendCodeCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async Task SendCodeCommandExecuted()
        {
            try
            {
                if (UserId == 0)
                    return;
                IsBussy = false;
                IsEnabledButton = Color.Parse("#E6EAEE");
                string codeString = $"{NumberUno}{NumberDos}{NumberTres}{NumberCuatro}{NumberCinco}";
                dialogService.LoadingAsync();
                var result = await serviceClient.SendCodeUser(UserId, new Models.User.SendCodeUserModel
                {
                    Code = codeString
                });
                dialogService.HideAsync();
                if (result != null && result.Result) 
                {
                    if (DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        //se ejecuta el roadcast
                        var receiver = DependencyService.Get<Barber.Colocho.App.Services.Receiver.IBroadcastSender>();
                        receiver.UnReceiver();
                    }
                    //se hiz el regstro
                    await message.ToastMessage(result?.Message);
                    await NavigationService.Instance.NavigationRoot();
                }
                else
                {
                    IsBussy = true;
                    IsEnabledButton = Color.Parse("#000000");
                    await message.SnackBar(result?.Message, SnackBarType.Error);
                }
            }
            catch(Exception ex)
            {

            }
        }
        public async Task ResendCodeCommandExecuted()
        {
            try
            {
                NumberCinco = string.Empty;
                NumberCuatro = string.Empty;
                NumberDos = string.Empty;
                NumberTres = string.Empty;
                NumberUno = string.Empty;
                dialogService.LoadingAsync();
                var result = await serviceClient.ResendCodeUser(UserId, new Models.User.SendCodeUserModel
                {
                    Code = "string"
                });
                dialogService.HideAsync();
                if (result != null && result.Result)
                {
                    await message.ToastMessage(result?.Message);
                }
                else
                    await message.SnackBar(result?.Message, SnackBarType.Error);
                IsBussy = false;
                IsEnabledColor = Color.Parse("#E6EAEE");
                IsEnabledButton = Color.Parse("#E6EAEE");
                IsEnabledResendCode = false;
                _ = OnTimer();
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region Methods
        private async Task OnTimer()
        {
            await Task.Run(() =>
            {
                int totalSeconds = 90;
                while (totalSeconds >= 0)
                {
                    int minutes = totalSeconds / 60;
                    int seconds = totalSeconds % 60;
                    Time = $"{minutes:D2}:{seconds:D2}";
                    Thread.Sleep(1000);  
                    totalSeconds--;
                }
                if(totalSeconds <= 0)
                {
                    IsEnabledResendCode = true;
                    IsEnabledColor = Color.Parse("#000000");
                }
            });
        }

        private async Task OnValidateNumber()
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(NumberUno) 
                    && !string.IsNullOrWhiteSpace(NumberDos) 
                    && !string.IsNullOrWhiteSpace(NumberTres) 
                    && !string.IsNullOrWhiteSpace(NumberCuatro)
                    && !string.IsNullOrWhiteSpace(NumberCinco))
                {
                    IsBussy = true;
                    IsEnabledColor = Color.Parse("#E6EAEE");
                    IsEnabledButton = Color.Parse("#000000");
                    IsEnabledResendCode = false;
                    await SendCodeCommandExecuted();
                }
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region Navigation Parameter

        private async Task OnRegister(AddUserModel parameter)
        {
            try
            {
                IsEnabledEntry = false;
                IsBussy = false;
                IsEnabledColor = Color.Parse("#E6EAEE");
                IsEnabledButton = Color.Parse("#E6EAEE");
                IsEnabledResendCode = false;
                if (parameter != null)
                {
                    var result = await serviceClient.Register(parameter);
                    if(result != null && result.Result)
                    {
                        IsEnabledEntry = true;
                        UserId = Convert.ToInt32(result.Count);
                        _ = OnTimer();
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
                        await NavigationService.Instance.GoBack();
                    }
                }
                else
                {
                    await dialogService.ModalPopup(Enums.DialogPopupEnum.Error, "Hubo un error, intente más tarde");
                }
            }
            catch(Exception ex)
            {

            }
        }
        #endregion
    }
}
