namespace Barber.Colocho.App.Services.Receiver
{
    public interface IBroadcastSender
    {
        Task<string> SendSMS();
        void UnReceiver();
    }
}
