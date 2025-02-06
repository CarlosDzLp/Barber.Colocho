using Barber.Colocho.App.Db;
using Barber.Colocho.App.Models.Company;
using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Navigation;
using Barber.Colocho.App.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Barber.Colocho.App.ViewModels.Pages.Company
{
    public class ListCompanyPageViewModel : BindableBase
    {
        IDialogService dialogService = new DialogService();
        IServiceClient serviceClient = new ServiceClient();

        #region Properties
        private List<CompanyModel> ListCompanyTemp {  get; set; }
        public ObservableCollection<CompanyModel> ListCompany { get; set; }
        private string _search;
        public string Search
        {
            get { return _search; }
            set
            {
                if(_search != value)
                {
                    SetProperty(ref _search, value);
                    OnSearchChange();
                }
            }
        }
        #endregion

        #region Constructor
        public ListCompanyPageViewModel()
        {
            AddCompanyCommand = new Command(async () => await AddCompanyCommandExecuted());
            DeleteCompanyCommand = new Command<CompanyModel>(async (e) => await DeleteCompanyCommandExecuted(e));
            EditAddressCommand = new Command<CompanyModel>(async (e) => await EditAddressCommandExecuted(e));
        }
        #endregion

        #region Command
        public ICommand AddCompanyCommand { get; set; }
        public ICommand DeleteCompanyCommand { get; set; }
        public ICommand EditAddressCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async Task EditAddressCommandExecuted(CompanyModel company)
        {
            try
            {
                if(company != null)
                {
                    await NavigationService.Instance.NavigateToPage(new Views.Page.Company.UpdateCompanyPage(company));
                }
            }
            catch(Exception ex)
            {

            }
        }
        private async Task DeleteCompanyCommandExecuted(CompanyModel company)
        {
            try
            {
                var user = DbContext.Instance.GetToken();
                if(company != null)
                {
                    dialogService.LoadingAsync();
                    var valdateSuscription = await serviceClient.SuscripcionActiveCompany(user.UserId, company.Id, $"Bearer {user.AccessToken}");
                    dialogService.HideAsync();
                    if (valdateSuscription != null && valdateSuscription.Result)
                    {
                        await dialogService.ModalPopup(Enums.DialogPopupEnum.Error, "No se puede eliminar la barbería, tiene una suscripción activa");
                    }
                    else
                    {
                        var result = await dialogService.BottomSheet(Enums.ConfirmPopupEnum.Delete, "¿Desea eliminar la barbería?");
                        if (result)
                        {
                            dialogService.LoadingAsync();
                            var response = await serviceClient.DeleteCompany(user.UserId, company.Id, $"Bearer {user.AccessToken}");
                            dialogService.HideAsync();
                            if (response != null && response.Result)
                            {
                                await dialogService.ModalPopup(Enums.DialogPopupEnum.Success, response?.Message);
                            }
                            else
                            {
                                await dialogService.ModalPopup(Enums.DialogPopupEnum.Error, response?.Message);
                            }
                            await OnLoadCompany();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                dialogService.HideAsync();
            }
        }
        private async Task AddCompanyCommandExecuted()
        {
            try
            {
                await NavigationService.Instance.NavigateToPage(new Views.Page.Company.AddCompanyPage());
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region Methods
        public async Task OnLoadCompany()
        {
            try
            {
                ListCompany = new ObservableCollection<CompanyModel>();
                var user = DbContext.Instance.GetToken();
                dialogService.LoadingAsync();
                var response = await serviceClient.GetListCompany(user.UserId, $"Bearer {user.AccessToken}");
                dialogService.HideAsync();
                if (response != null && response.Result != null && response.Result.Count > 0) 
                {
                    foreach (var item in response.Result) 
                    {
                        item.Image = (item.ListImage != null && item.ListImage.Count > 0) ? item.ListImage.FirstOrDefault().Name : "icon_cover.png";
                        item.Search = $"{item.Name} {item.RFC} {item.Address?.Name} {item.Address?.ColonyName} {item.Address?.CityName} {item.Address?.CodePostal} {item.Address?.NumExt} {item.Address?.Street} {item.Address?.StateName}";
                        item.AddressComplete = $"{item.Address?.Street} {item.Address?.NumExt} {item.Address?.ColonyName} {item.Address?.CodePostal} {item.Address?.CityName} {item.Address?.StateName}";
                        ListCompany.Add(item);
                    }
                    ListCompanyTemp = new List<CompanyModel>(ListCompany);
                }
            }
            catch(Exception ex)
            {
                dialogService.HideAsync();
            }
        }

        private void OnSearchChange()
        {
            try
            {
                ListCompany?.Clear();
                if (!string.IsNullOrWhiteSpace(Search)) 
                {
                    var filter = ListCompanyTemp.Where(c => c.AddressComplete.Contains(Search)).ToList();
                    ListCompany = new ObservableCollection<CompanyModel>(filter);
                }
                else
                {
                    ListCompany = new ObservableCollection<CompanyModel>(ListCompanyTemp);
                }
            }
            catch(Exception ex)
            {

            }
        }
        
        public async Task OnNavigationdetailPage(CompanyModel company)
        {
            try
            {
                if (company == null) return;
                await NavigationService.Instance.NavigateToPage(new Views.Page.Company.CompanyDetailPage(company));
            }
            catch(Exception ex)
            {

            }
        }
        #endregion
    }
}
