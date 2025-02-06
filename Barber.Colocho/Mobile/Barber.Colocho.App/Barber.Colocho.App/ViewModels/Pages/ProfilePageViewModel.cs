using Barber.Colocho.App.Db;
using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Navigation;
using Barber.Colocho.App.ViewModels.Base;
using System.Windows.Input;

namespace Barber.Colocho.App.ViewModels.Pages
{
    public class ProfilePageViewModel : BindableBase
    {
        private readonly IServiceClient serviceClient = new ServiceClient();
        private readonly IDialogService dialogService = new DialogService();

        #region Properties
        public string CompanyImage { get; set; } = "background_cover.png";
        //public string CompanyName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserImage { get; set; } = "icon_user.png";
        //public bool IsCompanyVisible { get; set; } = true;
        //public ObservableCollection<GenericModel> ListCompany { get; set; }
        //private GenericModel _company;
        //public GenericModel Company
        //{
        //    get
        //    {
        //        return _company;
        //    }
        //    set
        //    {
        //        _company = value;
        //        SetProperty(ref _company, value);
        //        if(_company != value)
        //        {
        //            OnChangeCompany();
        //        }
        //    }
        //}
        #endregion

        #region Constructor
        public ProfilePageViewModel()
        {
            AddAddressCommand = new Command(async () => await AddAddressCommandExecuted());
            EditProfileCommand = new Command(async () => await EditProfileCommandExecuted());
            LogOutCommand = new Command(async () => await LogOutCommandExecuted());
            DeleteAccountCommand = new Command(async () => await DeleteAccountCommandExecuted());
            ListCompanyCommand = new Command(async () => await ListCompanyCommandExecuted());
        }
        #endregion

        #region Command
        public ICommand AddAddressCommand { get; set; }
        public ICommand EditProfileCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        public ICommand ListCompanyCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async Task ListCompanyCommandExecuted()
        {
            try
            {
                await NavigationService.Instance.NavigateToPage(new Views.Page.Company.ListCompanyPage());
            }
            catch(Exception ex)
            {

            }
        }
        private async Task DeleteAccountCommandExecuted()
        {
            try
            {
                await NavigationService.Instance.NavigateToPage(new Views.Page.Profile.DeleteAccountPage());
            }
            catch(Exception ex)
            {

            }
        }
        private async Task LogOutCommandExecuted()
        {
            try
            {
                var result = await dialogService.BottomSheet(Enums.ConfirmPopupEnum.Danger, "¿Desea cerrar sesión?");
                if (result)
                {
                    DbContext.Instance.DeleteToken();
                    await NavigationService.Instance.OnBoarding();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private async Task EditProfileCommandExecuted()
        {
            try
            {
                await NavigationService.Instance.NavigateToPage(new Views.Page.Profile.EditProfilePage());
            }
            catch(Exception ex)
            {

            }
        }
        private async Task AddAddressCommandExecuted()
        {
            try
            {
                await NavigationService.Instance.NavigateToPage(new Views.Page.Address.ListAddressPage());
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region Methods
        public async Task LoadData()
        {
            try
            {
                //await LoadCompany();
                await LoadUser();
            }
            catch (Exception ex)
            {

            }
        }

        //private async Task OnChangeCompany()
        //{
        //    try
        //    {
        //        if(Company != null)
        //        {
        //            var db = DbContext.Instance.GetToken();
        //            var result = await serviceClient.GetCompany(db.UserId, Company.Id, db.AccessToken);
        //            if (result != null && result.Result != null) 
        //            {
        //                DbContext.Instance.UpdateToken(result.Result.Id);
        //                CompanyName = result.Result.Name;
        //                CompanyImage = result.Result.ListImage.FirstOrDefault().Name;
        //            }
        //        }

        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}

        //private async Task LoadCompany ()
        //{
        //    try
        //    {
        //        ListCompany = new ObservableCollection<GenericModel>();
        //        var db = DbContext.Instance.GetToken();
        //        var result = await serviceClient.GetListCompany(db.UserId, $"Bearer {db.AccessToken}");
        //        if (result != null && result.Result != null)
        //        {
        //            if (result.Result.Count > 0)
        //            {
        //                IsCompanyVisible = false;
        //                foreach (var item in result.Result)
        //                {
        //                    ListCompany.Add(new GenericModel
        //                    {
        //                        Id = item.Id,
        //                        Name = item.Name,
        //                    });
        //                }
        //                if (ListCompany.Count > 0)
        //                    Company = ListCompany.FirstOrDefault();
        //            }

        //            else
        //                IsCompanyVisible = true;
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}

        private async Task LoadUser()
        {
            try
            {
                var db = DbContext.Instance.GetToken();
                var result = await serviceClient.GetUser(db.UserId, $"Bearer {db.AccessToken}");
                if (result != null && result.Result != null)
                {
                    Name = result.Result.Name;
                    LastName = result.Result.LastName;
                    if(!string.IsNullOrWhiteSpace(result.Result.Image))
                        UserImage = result.Result.Image;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
