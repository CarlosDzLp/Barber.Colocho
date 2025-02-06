using Barber.Colocho.App.Db;
using Barber.Colocho.App.Models.Address;
using Barber.Colocho.App.Models.Base;
using Barber.Colocho.App.Models.Company;
using Barber.Colocho.App.Models.Suscription;
using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.ViewModels.Base;
using Maui.GoogleMaps;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Barber.Colocho.App.ViewModels.Pages.Company
{
    public class CompanyDetailPageViewModel : BindableBase
    {
        IDialogService dialogService = new DialogService();
        IServiceClient serviceClient = new ServiceClient();
        #region Properties
        public ObservableCollection<GenericModel> ListImage { get; set; }
        public CompanyModel Company { get; set; }
        public AddressModel Address { get; set; }
        public SuscriptionModel Suscription { get; set; }
        public string Suscribe {  get; set; }
        public bool IsSuscription {  get; set; }
        public bool IsVisibleSuscription { get; set; }
        public bool IsVisibleContentSuscripcion { get; set; }
        public ObservableCollection<Pin> Pins { get; set; }
        #endregion

        #region Constructor
        public CompanyDetailPageViewModel(CompanyModel company)
        {
            _ = OnLoadDetailCompany(company);
            SuscriptionCommand = new Command(async () => await SuscriptionCommandExecuted());
        }
        #endregion

        #region Command
        public ICommand SuscriptionCommand { get; set; }
        #endregion

        #region CommandExecuted
        private async Task SuscriptionCommandExecuted()
        {
            try
            {

            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region Methods
        private async Task OnLoadDetailCompany(CompanyModel company)
        {
            try
            {
                var user = DbContext.Instance.GetToken();
                if(company != null)
                {
                    dialogService.LoadingAsync();
                    var result = await serviceClient.GetCompany(user.UserId, company.Id, $"Bearer {user.AccessToken}");
                    dialogService.HideAsync();
                    if (result != null && result.Result != null) 
                    {
                        Company = result.Result;
                        Address = result.Result.Address;
                        Suscription = result.Result.Suscription;
                        Suscribe = (Suscription == null) ? "No" : "Si";
                        IsVisibleSuscription = (Suscription == null) ? true : false;
                        IsVisibleContentSuscripcion = (Suscription == null) ? false : true;
                        if (result.Result.ListImage != null && result.Result.ListImage.Count > 0)
                            ListImage = new ObservableCollection<GenericModel>(result.Result.ListImage);
                        if (ListImage == null || ListImage.Count == 0)
                        {
                            ListImage = new ObservableCollection<GenericModel>();
                            ListImage.Add(new GenericModel
                            {
                                Id = 1,
                                Name = "icon_cover.png"
                            });
                        }

                        if (Pins != null && Pins.Count > 0)
                        {
                            Pins.Clear();
                        }
                        Pins = new ObservableCollection<Pin>()
                        {
                            new Pin() {Position = new Position(Address.Latitude,Address.Longitude) },
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                dialogService.HideAsync();
            }
        }
        #endregion
    }
}
