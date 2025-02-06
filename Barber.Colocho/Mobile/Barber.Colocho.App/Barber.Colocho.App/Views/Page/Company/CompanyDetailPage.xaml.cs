using Barber.Colocho.App.Models.Company;
using Barber.Colocho.App.ViewModels.Pages.Company;

namespace Barber.Colocho.App.Views.Page.Company;

public partial class CompanyDetailPage : ContentPage
{
    CompanyDetailPageViewModel vm;
    public CompanyDetailPage(CompanyModel company)
	{
		InitializeComponent();
		this.BindingContext = vm = new CompanyDetailPageViewModel(company);
    }
}