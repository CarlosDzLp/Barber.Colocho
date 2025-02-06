using Barber.Colocho.App.ViewModels.Session;

namespace Barber.Colocho.App.Views.Session;

public partial class LoginPage : ContentPage
{
	LoginPageViewModel loginPageViewModel;
    public LoginPage()
	{
		InitializeComponent();
		this.BindingContext = loginPageViewModel = new LoginPageViewModel();
	}
}