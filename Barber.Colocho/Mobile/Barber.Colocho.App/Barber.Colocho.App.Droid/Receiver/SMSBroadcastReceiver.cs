using Android.Content;
using Android.Provider;
using System.Text.RegularExpressions;

namespace Barber.Colocho.App.Droid.Receiver
{
    [BroadcastReceiver(Enabled = true, Exported = true, Permission = "android.permission.RECEIVE_SMS")]
    public class SMSBroadcastReceiver : BroadcastReceiver
    {
        private const string SMS_RECEIVED = "android.provider.Telephony.SMS_RECEIVED";
        public override void OnReceive(Context? context, Intent? intent)
        {          
            if (intent.Action == SMS_RECEIVED)
            {
                var bundle = intent.Extras;
                if (bundle != null)
                {
                    var msgs = Telephony.Sms.Intents.GetMessagesFromIntent(intent);
                    string number = string.Empty;
                    foreach (var item in msgs)
                    {
                        string numbers = Regex.Replace(item.DisplayMessageBody, @"\D", "");
                        number += numbers;
                    }
                    if (!string.IsNullOrEmpty(number)) 
                    {
                        BroadcastSender.OnRequestReceiverResult(number);
                    }                  
                }
            }
        }
    }
}
