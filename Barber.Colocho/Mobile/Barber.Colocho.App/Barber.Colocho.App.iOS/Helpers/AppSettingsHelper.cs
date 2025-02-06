using Barber.Colocho.App.Services.Settings;
using Foundation;
using UIKit;

namespace Barber.Colocho.App.iOS.Helpers
{
    public class AppSettingsHelper : IAppSettingsHelper
    {
        public void OpenAppSettings()
        {
            try
            {
                var url = new NSUrl($"app-settings:");
                UIApplication.SharedApplication.OpenUrl(url);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
