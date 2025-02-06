using Barber.Colocho.App.ViewModels.Pages.Company;

namespace Barber.Colocho.App.Views.Page.Company;

public partial class AddCompanyPage : ContentPage
{
	AddCompanyPageViewModel vm;
    public AddCompanyPage()
	{
		InitializeComponent();
		this.BindingContext = vm = new AddCompanyPageViewModel();
    }
}