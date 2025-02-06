namespace Barber.Colocho.App.Services.Message
{
    public interface IMessageDialog
    {
        Task ToastMessage(string message);
        Task SnackBar(string message, SnackBarType snackBarType);
    }

    public enum SnackBarType
    {
        Error,
        Success,
    }
}
