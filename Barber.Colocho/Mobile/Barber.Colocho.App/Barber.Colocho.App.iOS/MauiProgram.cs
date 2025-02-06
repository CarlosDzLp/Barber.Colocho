using Barber.Colocho.App.Helpers;
using Maui.GoogleMaps.Hosting;
using Maui.GoogleMaps.iOS;
using Mopups.Hosting;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Barber.Colocho.App.iOS
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.ConfigureEffects(effects =>
            {
                effects.Add<Barber.Colocho.App.Effects.UnderlineEntryEffect, Barber.Colocho.App.iOS.Effects.UnderlinePlatformEntryEffect>();
            });
            builder.UseSharedMauiApp();
            builder.UseSkiaSharp();
            var platformCaching = new PlatformConfig()
            {
                ImageFactory = new CachingImageFactory()
            };
            builder.UseGoogleMaps(GoogleMapsKey.KeyiOS, platformCaching);
            builder.ConfigureMopups();
            DependencyService.Register<Barber.Colocho.App.iOS.Helpers.AppSettingsHelper>();
            return builder.Build();
        }
    }
}
