using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.Authenticate
{
    public class AuthenticateModel
    {
        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
