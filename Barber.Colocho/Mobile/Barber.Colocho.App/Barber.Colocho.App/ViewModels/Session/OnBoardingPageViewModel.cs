using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Navigation;
using Barber.Colocho.App.ViewModels.Base;
using Barber.Colocho.App.Views.Session;
using System.Windows.Input;

namespace Barber.Colocho.App.ViewModels.Session
{
    public class OnBoardingPageViewModel : BindableBase
    {
        private readonly IDialogService dialogService = new DialogService();
        #region Constructor
        public OnBoardingPageViewModel()
        {
            LoginCommand = new Command(async () => await LoginCommandExecuted());
            RegisterCommand = new Command(async () => await RegisterCommandExecuted());
        }
        #endregion

        #region Command
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async Task LoginCommandExecuted()
        {
            try
            {
                await NavigationService.Instance.NavigateToPage(new LoginPage());
            }
            catch (Exception ex) 
            {
            }
        }

        private async Task RegisterCommandExecuted()
        {
            try
            {
                await NavigationService.Instance.NavigateToPage(new RegisterPage());
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}
