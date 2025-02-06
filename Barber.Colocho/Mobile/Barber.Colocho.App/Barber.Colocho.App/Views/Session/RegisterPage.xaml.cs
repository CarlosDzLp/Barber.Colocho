using Barber.Colocho.App.ViewModels.Session;

namespace Barber.Colocho.App.Views.Session;

public partial class RegisterPage : ContentPage
{
	RegisterPageViewModel registerPageViewModel;
    public RegisterPage()
	{
		InitializeComponent();
		this.BindingContext = registerPageViewModel = new RegisterPageViewModel();
	}
}