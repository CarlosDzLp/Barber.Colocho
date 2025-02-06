using Maui.GoogleMaps.Android;
using Maui.GoogleMaps.Hosting;
using Mopups.Hosting;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Barber.Colocho.App.Droid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.ConfigureEffects(effects =>
            {
                effects.Add<Barber.Colocho.App.Effects.UnderlineEntryEffect, Barber.Colocho.App.Droid.Effects.UnderlinePlatformEntryEffect>();
            });
            builder.ConfigureMauiHandlers(handlers => {
                handlers.AddHandler<Barber.Colocho.App.Controls.CustomTabbedPage, Barber.Colocho.App.Droid.Controls.CustomTabbedPageHandler>();
            });
            builder.UseSharedMauiApp();
            builder.ConfigureMopups();
            builder.UseSkiaSharp();
            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            };


            builder.UseGoogleMaps(platformConfig);
            DependencyService.Register<Barber.Colocho.App.Droid.Receiver.BroadcastSender>();
            DependencyService.Register<Barber.Colocho.App.Droid.Helpers.AppSettingsHelper>();
            return builder.Build();
        }
    }
}
