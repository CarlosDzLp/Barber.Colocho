using Barber.Colocho.App.Models.Address;
using Barber.Colocho.App.Models.Filter;
using Barber.Colocho.Enums;

namespace Barber.Colocho.App.Services.Dialog
{
    public interface IDialogService
    {
        void LoadingAsync(string message = null);
        void HideAsync();
        Task<FilterModel> FilterMap(double latitude, double longitude);
        Task<bool> BottomSheet(ConfirmPopupEnum confirm, string message);
        Task<bool> ModalPopup(DialogPopupEnum confirm, string message);
        Task<Maui.GoogleMaps.Position?> LocationPopup();
        Task<bool> PermissionPopup(string message, string img);

        Task<AddressModel> AddressPopup(int addressId = 0);
    }
}
