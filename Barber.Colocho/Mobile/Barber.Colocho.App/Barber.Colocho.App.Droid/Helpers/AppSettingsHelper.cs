using Android.Content;
using Barber.Colocho.App.Services.Settings;

namespace Barber.Colocho.App.Droid.Helpers
{
    public class AppSettingsHelper : IAppSettingsHelper
    {
        public void OpenAppSettings()
        {
            try
            {
                var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
                intent.AddFlags(ActivityFlags.NewTask);
                string package_name = Platform.CurrentActivity.ApplicationContext.PackageName;
                var uri = Android.Net.Uri.FromParts("package", package_name, null);
                intent.SetData(uri);
                Platform.CurrentActivity.StartActivity(intent);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
