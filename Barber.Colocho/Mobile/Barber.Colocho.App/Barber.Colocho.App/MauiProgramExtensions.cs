using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Message;
using Barber.Colocho.App.Services.Permissions;
using Barber.Colocho.App.ViewModels.Pages;
using Barber.Colocho.App.ViewModels.Pages.Address;
using Barber.Colocho.App.ViewModels.Pages.Profile;
using Barber.Colocho.App.ViewModels.Session;
using Barber.Colocho.App.Views.Page;
using Barber.Colocho.App.Views.Page.Address;
using Barber.Colocho.App.Views.Page.Profile;
using Barber.Colocho.App.Views.Popups;
using Barber.Colocho.App.Views.Session;
using CommunityToolkit.Maui;
using IeuanWalker.Maui.Switch;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;

namespace Barber.Colocho.App
{
    public static class MauiProgramExtensions
    {
        public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
        {
            builder
                .UseMauiApp<App>()
                .UseSwitch()
                .UseMauiCommunityToolkit()
                .RegisterViewModels()
                .RegisterViews()
                .RegisterService()
                .UseMauiCompatibility()
                .ConfigureFonts(fonts =>
                {
                    //FONT
                    fonts.AddFont("Roboto-Black.ttf", "RobotoBlack");
                    fonts.AddFont("Roboto-BlackItalic.ttf", "RobotoBlackItalic");
                    fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                    fonts.AddFont("Roboto-BoldItalic.ttf", "RobotoBoldItalic");
                    fonts.AddFont("Roboto-Italic.ttf", "RobotoItalic");
                    fonts.AddFont("Roboto-Light.ttf", "RobotoLight");
                    fonts.AddFont("Roboto-LightItalic.ttf", "RobotoLightItalic");
                    fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
                    fonts.AddFont("Roboto-MediumItalic.ttf", "RobotoMediumItalic");
                    fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                    fonts.AddFont("Roboto-Thin.ttf", "RobotoThin");
                    fonts.AddFont("Roboto-ThinItalic.ttf", "RobotoThinItalic");

                    //FONT AWESOME
                    fonts.AddFont("FontAwesome-Brands-Regular.otf", "FBrands-Regular");
                    fonts.AddFont("FontAwesome-Duotone-Solid.otf", "FDuotone-Solid");
                    fonts.AddFont("FontAwesome-Pro-Light.otf", "FPro-Light");
                    fonts.AddFont("FontAwesome-Pro-Regular.otf", "FPro-Regular");
                    fonts.AddFont("FontAwesome-Pro-Solid.otf", "FPro-Solid");
                    fonts.AddFont("FontAwesome-Pro-Thin.otf", "FPro-Thin");
                    fonts.AddFont("FontAwesome-Sharp-Light.otf", "FSharp-Light");
                    fonts.AddFont("FontAwesome-Sharp-Regular.otf", "FSharp-Regular");
                    fonts.AddFont("FontAwesome-Sharp-Solid.otf", "FSharp-Solid");
                    fonts.AddFont("FontAwesome-Sharp-Thin.otf", "FSharp-Thin");
                });

            

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder;
        }




        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            //Navigations
            //mauiAppBuilder.Services.AddSingleton<NavigationService>();

            //Pages
            //mauiAppBuilder.Services.AddSingleton<OnBoardingPage>();
            //mauiAppBuilder.Services.AddSingleton<LoginPage>();
            //mauiAppBuilder.Services.AddSingleton<RegisterPage>();
            //mauiAppBuilder.Services.AddSingleton<VerificationCodePage>();
            //mauiAppBuilder.Services.AddSingleton<BarberPage>();
            //mauiAppBuilder.Services.AddSingleton<HomePage>();
            //mauiAppBuilder.Services.AddSingleton<ExplorePage>();
            //mauiAppBuilder.Services.AddSingleton<CalendarPage>();
            //mauiAppBuilder.Services.AddSingleton<ProfilePage>();
            //mauiAppBuilder.Services.AddSingleton<ForgotPasswordPage>();
            //mauiAppBuilder.Services.AddSingleton<ListAddressPage>();
            //mauiAppBuilder.Services.AddSingleton<AddAddressPage>();
            //mauiAppBuilder.Services.AddSingleton<UpdateAddressPage>();
            //mauiAppBuilder.Services.AddSingleton<LocationPage>();
            //mauiAppBuilder.Services.AddSingleton<EditProfilePage>();
            //mauiAppBuilder.Services.AddSingleton<FilterPage>();

            //Popups
            //mauiAppBuilder.Services.AddSingleton<LoadingPage>();
            //mauiAppBuilder.Services.AddSingleton<PermissionPopup>();
            //mauiAppBuilder.Services.AddSingleton<DialogBottomSheet>();
            //mauiAppBuilder.Services.AddSingleton<DialogPopup>();
            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            //ViewModels
            //mauiAppBuilder.Services.AddSingleton<OnBoardingPageViewModel>();
            //mauiAppBuilder.Services.AddSingleton<LoginPageViewModel>();
            //mauiAppBuilder.Services.AddSingleton<RegisterPageViewModel>();
            //mauiAppBuilder.Services.AddSingleton<VerificationCodePageViewModel>();
            //mauiAppBuilder.Services.AddSingleton<ForgotPasswordPageViewModel>();
            //mauiAppBuilder.Services.AddSingleton<ProfilePageViewModel>();
            //mauiAppBuilder.Services.AddSingleton<ListAddressPageViewModel>();
            //mauiAppBuilder.Services.AddSingleton<AddAddressPageViewModel>();
            //mauiAppBuilder.Services.AddSingleton<UpdateAddressPageViewModel>();
            //mauiAppBuilder.Services.AddSingleton<EditProfilePageViewModel>();
            //mauiAppBuilder.Services.AddSingleton<ExplorePageViewModel>();
            //mauiAppBuilder.Services.AddSingleton<CalendarPageViewModel>();
            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterService(this MauiAppBuilder mauiAppBuilder)
        {
            //Service
            //mauiAppBuilder.Services.AddSingleton<IDialogService, DialogService>();
            //mauiAppBuilder.Services.AddSingleton<IServiceClient, ServiceClient>();
            //mauiAppBuilder.Services.AddSingleton<HttpClientMessageHandler>();
            //mauiAppBuilder.Services.AddSingleton<IMessage, Message>();
            //mauiAppBuilder.Services.AddSingleton<CheckPermissions>();
            return mauiAppBuilder;
        }
    }
}
