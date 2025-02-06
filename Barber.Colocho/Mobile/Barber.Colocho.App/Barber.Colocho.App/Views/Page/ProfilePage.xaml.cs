using Barber.Colocho.App.ViewModels.Pages;

namespace Barber.Colocho.App.Views.Page;

public partial class ProfilePage : ContentPage
{
	ProfilePageViewModel profilePageView;
    public ProfilePage()
	{
		InitializeComponent();
		this.BindingContext = profilePageView = new ProfilePageViewModel();
	}

    protected async override void OnAppearing()
    {
		await profilePageView.LoadData();
        base.OnAppearing();
    }
}