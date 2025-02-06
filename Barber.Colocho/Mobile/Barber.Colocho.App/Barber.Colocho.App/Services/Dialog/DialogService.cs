using Barber.Colocho.App.Models.Address;
using Barber.Colocho.App.Models.Filter;
using Barber.Colocho.App.Views.Page;
using Barber.Colocho.App.Views.Page.Company;
using Barber.Colocho.App.Views.Popups;
using Barber.Colocho.Enums;
using Maui.GoogleMaps;
using Mopups.Services;

namespace Barber.Colocho.App.Services.Dialog
{
    public class DialogService : IDialogService
    {
        #region BottomSheet
        TaskCompletionSource<bool> _bottomSheetTask;
        DialogBottomSheet _bottomSheet;
        public async Task<bool> BottomSheet(ConfirmPopupEnum confirm, string message)
        {
            try
            {
                _bottomSheetTask = new TaskCompletionSource<bool>();
                _bottomSheet = new DialogBottomSheet(confirm, message);
                _bottomSheet.Confirm += _bottomSheet_Confirm;
                await MopupService.Instance.PushAsync(_bottomSheet);
                return await _bottomSheetTask.Task;
            }
            catch(Exception ex)
            {
                _bottomSheet = null;
                _bottomSheetTask = null;
                return false;
            }
        }

        private void _bottomSheet_Confirm(object? sender, bool e)
        {
            try
            {
                _bottomSheet.Confirm -= _bottomSheet_Confirm;
                _bottomSheet = null;
                _bottomSheetTask.TrySetResult(e);
            }
            catch(Exception ex)
            {
                _bottomSheetTask.TrySetResult(false);
            }
        }
        #endregion

        #region Filter Map
        FilterPage _filterPage;
        TaskCompletionSource<FilterModel> _filterPageTask;
        public async Task<FilterModel> FilterMap(double latitude, double longitude)
        {
            try
            {
                _filterPageTask = new TaskCompletionSource<FilterModel>();
                _filterPage = new FilterPage(latitude, longitude);
                _filterPage.FilterChanged += _filterPage_FilterChanged;
                var page = Application.Current?.MainPage ?? throw new NullReferenceException();
                await page.Navigation.PushModalAsync(_filterPage);
                return await _filterPageTask.Task;
            }
            catch (Exception ex)
            {
                _filterPageTask = null;
                _filterPage = null;
                return null;
            }
        }

        private void _filterPage_FilterChanged(object? sender, FilterModel e)
        {
            try
            {
                _filterPage.FilterChanged -= _filterPage_FilterChanged;
                _filterPage = null;
                _filterPageTask.TrySetResult(e);
            }
            catch(Exception ex)
            {
                _filterPage = null;
                _filterPageTask.TrySetResult(null);              
            }
        }
        #endregion

        #region Modal Popup
        TaskCompletionSource<bool> _modalPopup;
        Views.Popups.DialogPopup _dialogPopup;
        public async Task<bool> ModalPopup(DialogPopupEnum confirm, string message)
        {
            try
            {
                _modalPopup = new TaskCompletionSource<bool>();
                _dialogPopup = new Views.Popups.DialogPopup(confirm, message);
                _dialogPopup.Confirm += _dialogPopup_Confirm;
                await MopupService.Instance.PushAsync(_dialogPopup);
                return await _modalPopup.Task;
            }
            catch (Exception ex)
            {
                _modalPopup = null;
                _dialogPopup = null;
                return false;
            }
        }

        private void _dialogPopup_Confirm(object? sender, bool e)
        {
            try
            {
                _dialogPopup.Confirm -= _dialogPopup_Confirm;
                _dialogPopup = null;
                _modalPopup.TrySetResult(e);
            }
            catch(Exception ex)
            {
                _dialogPopup = null;
                _modalPopup.TrySetResult(false);
            }
        }
        #endregion

        #region Permission
        TaskCompletionSource<bool> _permissionTask;
        PermissionPopup _permissionPopup;
        public async Task<bool> PermissionPopup(string message, string img)
        {
            try
            {
                _permissionTask = new TaskCompletionSource<bool>();
                _permissionPopup = new PermissionPopup(message, img);
                _permissionPopup.OnPermissionPopup += _permissionPopup_OnPermissionPopup;
                await MopupService.Instance.PushAsync(_permissionPopup);
                return await _permissionTask.Task;
            }
            catch(Exception ex)
            {
                _permissionTask = null;
                _permissionPopup = null;
                return false;
            }
        }

        private void _permissionPopup_OnPermissionPopup(object? sender, bool e)
        {
            try
            {
                _permissionPopup.OnPermissionPopup -= _permissionPopup_OnPermissionPopup;
                _permissionPopup = null;
                _permissionTask.TrySetResult(e);
            }
            catch(Exception ex)
            {
                _permissionPopup = null;
                _permissionTask.TrySetResult(false);
            }
        }
        #endregion

        #region Position Popup
        TaskCompletionSource<Position?> _positionTask;
        LocationPage _locationPage;
        public async Task<Position?> LocationPopup()
        {
            try
            {
                _positionTask = new TaskCompletionSource<Position?>();
                _locationPage = new LocationPage();
                _locationPage.LocationChanged += _locationPage_LocationChanged;
                var page = App.Current.MainPage;
                await page.Navigation.PushAsync(_locationPage);
                return await _positionTask.Task;
            }
            catch(Exception ex)
            {
                _positionTask = null;
                _locationPage = null;
                return null;
            }
        }

        private void _locationPage_LocationChanged(object? sender, Position? e)
        {
            try
            {
                _locationPage.LocationChanged -= _locationPage_LocationChanged;
                _locationPage = null;
                _positionTask.TrySetResult(e);
            }
            catch(Exception ex)
            {
                _locationPage = null;
                _positionTask.TrySetResult(null);
            }
        }
        #endregion

        #region Loading
        LoadingPage _loadingPage;
        public void HideAsync()
        {
            try
            {
                MopupService.Instance.RemovePageAsync(_loadingPage);
                _loadingPage = null;
            }
            catch(Exception ex)
            {
                _loadingPage = null;
            }
        }

        public void LoadingAsync(string message = null)
        {
            try
            {
                _loadingPage = new LoadingPage(message);
                MopupService.Instance.PushAsync(_loadingPage);
            }
            catch(Exception ex)
            {
                _loadingPage = null;
            }
        }
        #endregion

        #region AddressPopup
        ListAddressCompanyPage _addressPage;
        TaskCompletionSource<AddressModel> _addresPageTask;
        public async Task<AddressModel> AddressPopup(int addressId = 0)
        {
            try
            {
                _addresPageTask = new TaskCompletionSource<AddressModel>();
                _addressPage = new ListAddressCompanyPage(addressId);
                _addressPage.AddressChanged += _addressPage_AddressChanged;
                var page = Application.Current?.MainPage ?? throw new NullReferenceException();
                await page.Navigation.PushModalAsync(_addressPage);
                return await _addresPageTask.Task;
            }
            catch (Exception ex)
            {
                _addresPageTask = null;
                _addressPage = null;
                return null;
            }
        }

        private void _addressPage_AddressChanged(object? sender, AddressModel? e)
        {
            try
            {
                _addressPage.AddressChanged -= _addressPage_AddressChanged;
                _addressPage = null;
                _addresPageTask.TrySetResult(e);
            }
            catch (Exception ex)
            {
                _addressPage = null;
                _addresPageTask.TrySetResult(null);
            }
        }
        #endregion
    }
}
