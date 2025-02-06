using Android.App;
using Android.Runtime;
using Barber.Colocho.App.Helpers;

namespace Barber.Colocho.App.Droid
{
#if DEBUG                                   // connect to local service on the
    [Application(UsesCleartextTraffic = true)]  // emulator's host for debugging,
#else                                       // access via http://10.0.2.2
[Application]
#endif
    [MetaData("com.google.android.maps.v2.API_KEY",
            Value = GoogleMapsKey.KeyDroid)]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
