using Barber.Colocho.App.ViewModels.Pages.Profile;

namespace Barber.Colocho.App.Views.Page.Profile;

public partial class EditProfilePage : ContentPage
{
	EditProfilePageViewModel pageViewModel;
    public EditProfilePage()
	{
		InitializeComponent();
		this.BindingContext = pageViewModel = new EditProfilePageViewModel();
	}
}