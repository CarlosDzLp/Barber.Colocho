using Barber.Colocho.App.ViewModels.Session;

namespace Barber.Colocho.App.Views.Session;

public partial class OnBoardingPage : ContentPage
{
    OnBoardingPageViewModel onBoardingPageViewModel;
    public OnBoardingPage()
	{
		InitializeComponent();
        this.BindingContext = onBoardingPageViewModel = new OnBoardingPageViewModel();
    }
}