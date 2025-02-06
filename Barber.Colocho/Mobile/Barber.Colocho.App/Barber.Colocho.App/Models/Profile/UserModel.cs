using Barber.Colocho.Enums;
using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.Profile
{
    public class UserModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("RolName")]
        public RolesEnum RolName { get; set; }
     
        [JsonProperty("Image")]
        public string Image { get; set; }
    }
}
