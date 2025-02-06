using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.User
{
    public class SendCodeForgotPasswordModel
    {
        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }
    }
}
