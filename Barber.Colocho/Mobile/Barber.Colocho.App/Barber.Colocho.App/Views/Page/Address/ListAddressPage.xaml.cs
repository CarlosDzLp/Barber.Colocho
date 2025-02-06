using Barber.Colocho.App.ViewModels.Pages.Address;

namespace Barber.Colocho.App.Views.Page.Address;

public partial class ListAddressPage : ContentPage
{
	ListAddressPageViewModel listAddressPageViewModel;
    public ListAddressPage()
	{
		InitializeComponent();
        this.BindingContext = listAddressPageViewModel = new ListAddressPageViewModel();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_ = listAddressPageViewModel.LoadListAddress();
    }

    protected override bool OnBackButtonPressed()
    {
        return listAddressPageViewModel.CloseBottomSheet();
    }
}