using Barber.Colocho.App.Models.Company;
using Barber.Colocho.App.ViewModels.Pages.Company;

namespace Barber.Colocho.App.Views.Page.Company;

public partial class UpdateCompanyPage : ContentPage
{
    UpdateCompanyPageViewModel vm;
    public UpdateCompanyPage(CompanyModel company)
	{
		InitializeComponent();
		this.BindingContext = vm = new UpdateCompanyPageViewModel(company);

    }
}