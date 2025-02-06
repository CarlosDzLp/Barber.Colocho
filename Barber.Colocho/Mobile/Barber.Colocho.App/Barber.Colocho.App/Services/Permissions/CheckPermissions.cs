using Barber.Colocho.App.Services.Dialog;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace Barber.Colocho.App.Services.Permissions
{
    public class CheckPermissions
    {
        private readonly IDialogService dialogService = new DialogService();
        public CheckPermissions()
        {
            
        }

        public async Task<PermissionStatus> CheckAndRequestPermissionAsync<T>(T permission, string text,string image) where T : BasePermission
        {
            var status = await permission.CheckStatusAsync();
            if (status == PermissionStatus.Granted)
                return status;

            if(status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                return status;
            }
            bool isViewPermission = permission.ShouldShowRationale();
            if (isViewPermission)
            {
                await dialogService.PermissionPopup(text, image);
            }
            status = await permission.RequestAsync();
            return status;
        }

        public void OpenSettings()
        {
            try
            {
                var settings = DependencyService.Get<Barber.Colocho.App.Services.Settings.IAppSettingsHelper>();
                settings.OpenAppSettings();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
