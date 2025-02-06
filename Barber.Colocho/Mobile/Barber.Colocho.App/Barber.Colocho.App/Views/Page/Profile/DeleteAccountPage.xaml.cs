using Barber.Colocho.App.Db;
using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Navigation;
using System.Runtime.InteropServices;

namespace Barber.Colocho.App.Views.Page.Profile;

public partial class DeleteAccountPage : ContentPage
{
	IServiceClient serviceClient = new ServiceClient();
	IDialogService dialogService = new DialogService();
	public DeleteAccountPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		try
		{
			var result = await dialogService.BottomSheet(Enums.ConfirmPopupEnum.Danger, "¿Desea eliminar la cuenta?");
			if (result)
			{
				var user = DbContext.Instance.GetToken();
				dialogService.LoadingAsync();
				var response = await serviceClient.DeleteAccount(user.UserId, $"Bearer {user.AccessToken}");
				dialogService.HideAsync();
                if (response != null && response.Result)
                {
					await dialogService.ModalPopup(Enums.DialogPopupEnum.Success, "Se ha elminado la cuenta correctamente");
					DbContext.Instance.DeleteToken();
					await NavigationService.Instance.OnBoarding();
                }
                else
                {
					await dialogService.ModalPopup(Enums.DialogPopupEnum.Error, response?.Message);	
				}
            }
		}
		catch(Exception ex)
		{
			dialogService.HideAsync();
		}
    }
}