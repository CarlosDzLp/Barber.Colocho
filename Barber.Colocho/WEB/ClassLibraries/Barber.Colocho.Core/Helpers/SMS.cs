using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Barber.Colocho.Core.Helpers
{
    public class SMS
    {
        public static async Task SendTwilio(string phone,string code)
        {
            
            var accountSid = "AC50f07e68495fb0d1c01e1f9ad11ac32d";
            var authToken = "4d5b593591acf24b9288513abc943deb";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
              new PhoneNumber($"+52{phone}"));
            messageOptions.From = new PhoneNumber("+13347588917");
            messageOptions.Body = $"Le compartimos el código para su registro: {code}";

            var message = await MessageResource.CreateAsync(messageOptions);
        }
    }
}
