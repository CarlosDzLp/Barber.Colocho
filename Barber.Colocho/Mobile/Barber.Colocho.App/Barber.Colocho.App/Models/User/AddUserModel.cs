using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.User
{
    public class AddUserModel
    {
        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
