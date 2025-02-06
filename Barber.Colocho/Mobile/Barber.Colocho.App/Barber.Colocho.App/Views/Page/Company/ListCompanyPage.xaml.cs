using Barber.Colocho.App.Models.Company;
using Barber.Colocho.App.ViewModels.Pages.Company;

namespace Barber.Colocho.App.Views.Page.Company;

public partial class ListCompanyPage : ContentPage
{
	private ListCompanyPageViewModel vm;
    public ListCompanyPage()
	{
		InitializeComponent();
		this.BindingContext = vm = new ListCompanyPageViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = vm.OnLoadCompany();
    }

    private async void collectionviewCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0) 
            {
                var item = e.CurrentSelection.FirstOrDefault() as CompanyModel;
                if (item != null)
                {
                    await vm.OnNavigationdetailPage(item); 
                }
            }
            ((CollectionView)sender).SelectedItem = null;
        }
        catch(Exception ex)
        {

        }
    }
}