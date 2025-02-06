using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.User
{
    public class ForgotPasswordModel
    {
        [JsonProperty("UserId")]
        public int UserId { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }
    }
}
