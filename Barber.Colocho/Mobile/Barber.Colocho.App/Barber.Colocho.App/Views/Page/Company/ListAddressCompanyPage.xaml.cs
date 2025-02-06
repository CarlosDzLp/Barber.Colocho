using Barber.Colocho.App.Db;
using Barber.Colocho.App.Helpers;
using Barber.Colocho.App.Models.Address;
using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Barber.Colocho.App.Views.Page.Company;

public partial class ListAddressCompanyPage : ContentPage, INotifyPropertyChanged
{
	IDialogService dialogService = new DialogService();
	IServiceClient serviceClient = new ServiceClient();
    public event EventHandler<AddressModel?> AddressChanged;
    #region NotifyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (object.Equals(field, value)) { return false; }

        field = value;
        this.OnPropertyChanged(propertyName);
        return true;
    }
    #endregion

    #region Properties
    public List<AddressModel> ListAddressTemp { get; set; }
    public ObservableCollection<AddressModel> ListAddress { get; set; }
    private string _search;
    public string Search
    {
        get { return _search; }
        set
        {
            if (_search != value)
            {
                SetProperty(ref _search, value);
                OnSearchChange();
            }
        }
    }
    #endregion

    public ListAddressCompanyPage(int AddresId = 0)
	{
		InitializeComponent();
		this.BindingContext = this;
        _ = LoadListAddress(AddresId);
    }

    #region Methods
    private async Task LoadListAddress(int addressId)
    {
        try
        {
            ListAddress = new ObservableCollection<AddressModel>();
            ListAddress.Clear();
            var user = DbContext.Instance.GetToken();
            if (user == null)
                return;
            var result = await serviceClient.GetListAddressByUserId(user.UserId, $"Bearer {user.AccessToken}");
            if (result != null && result.Result.Count > 0)
            {
                foreach (var item in result.Result)
                {
                    item.IsDefault = (addressId > 0 && addressId == item.Id) ? true : false;
                    item.FontFamily = (item.IsDefault) ? FontAwesome.ProSolid : FontAwesome.ProLight;
                    item.Search = $"{item.Name} {item.ColonyName} {item.CityName} {item.CodePostal} {item.NumExt} {item.Street} {item.StateName}";
                    ListAddress.Add(item);
                }
                ListAddressTemp = new List<AddressModel>(ListAddress);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void OnSearchChange()
    {
        try
        {
            ListAddress?.Clear();
            ListAddress = null;
            if (!string.IsNullOrWhiteSpace(Search))
            {
                var filter = ListAddressTemp.Where(c => c.Search.Contains(Search)).ToList();
                ListAddress = new ObservableCollection<AddressModel>(filter);
            }
            else
            {
                ListAddress = new ObservableCollection<AddressModel>(ListAddressTemp);
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected override bool OnBackButtonPressed()
    {
        AddressChanged?.Invoke(this, null);
        return base.OnBackButtonPressed();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        
        App.Current.MainPage.Navigation.PopModalAsync();
        AddressChanged?.Invoke(this, null);
    }
    #endregion

    private void collectionviewAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if(e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var item = e.CurrentSelection.FirstOrDefault() as AddressModel;
                if(item != null)
                {
                    AddressChanged?.Invoke(this, item);
                    App.Current.MainPage.Navigation.PopModalAsync();
                }
                ((CollectionView)sender).SelectedItem = null;
            }
        }
        catch(Exception ex)
        {

        }
    }
}