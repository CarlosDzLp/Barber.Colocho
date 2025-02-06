using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;

namespace Barber.Colocho.App.Services.Message
{
    public class MessageDialog : IMessageDialog
    {
        public async Task SnackBar(string message, SnackBarType snackBarType)
        {
            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = (snackBarType == SnackBarType.Error) ? Colors.Red : Color.Parse("#35C2C1"),
                TextColor = Colors.White,
                CornerRadius = new CornerRadius(10),
                Font = Font.SystemFontOfSize(16, FontWeight.Regular, FontSlant.Default, true)
            };
            TimeSpan duration = TimeSpan.FromSeconds(3);
            var snackbar = Snackbar.Make(message, actionButtonText: string.Empty, duration: duration, visualOptions: snackbarOptions);
            await snackbar.Show();
        }

        public async Task ToastMessage(string message)
        {
            ToastDuration duration = ToastDuration.Long;
            double fontSize = 16;
            var toast = Toast.Make(message, duration, fontSize);
            await toast.Show();
        }
    }
}
