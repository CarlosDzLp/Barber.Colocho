using Android.Content;
using Barber.Colocho.App.Services.Receiver;

namespace Barber.Colocho.App.Droid.Receiver
{
    public class BroadcastSender : IBroadcastSender
    {
        static SMSBroadcastReceiver sMSBroadcastReceiver = null;
        static TaskCompletionSource<string> mediaPermissionTcs;
        public async Task<string> SendSMS()
        {
            mediaPermissionTcs = new TaskCompletionSource<string>();
            sMSBroadcastReceiver = new SMSBroadcastReceiver();
            var intentFilter = new IntentFilter("android.provider.Telephony.SMS_RECEIVED");
            intentFilter.Priority = (int)IntentFilterPriority.HighPriority;
            Platform.CurrentActivity.RegisterReceiver(sMSBroadcastReceiver, intentFilter);
            return await mediaPermissionTcs.Task;
        }

        public static void OnRequestReceiverResult(string code)
        {
            try
            {
                Platform.CurrentActivity.UnregisterReceiver(sMSBroadcastReceiver);
                if (!string.IsNullOrWhiteSpace(code)) 
                {
                    mediaPermissionTcs.TrySetResult(code);
                }
            }
            catch(Exception ex)
            {
                mediaPermissionTcs.TrySetResult(null);
            }
        }

        public void UnReceiver()
        {
            try
            {
                if(sMSBroadcastReceiver != null)
                    Platform.CurrentActivity.UnregisterReceiver(sMSBroadcastReceiver);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
