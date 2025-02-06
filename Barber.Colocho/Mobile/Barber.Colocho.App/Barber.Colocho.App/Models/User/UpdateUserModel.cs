using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.User
{
    public class UpdateUserModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }
    }
}
