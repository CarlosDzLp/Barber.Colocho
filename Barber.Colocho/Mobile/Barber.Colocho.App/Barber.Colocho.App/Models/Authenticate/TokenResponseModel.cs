using Newtonsoft.Json;
using SQLite;

namespace Barber.Colocho.App.Models.Authenticate
{
    public class TokenResponseModel
    {
        [JsonProperty("UserId")]
        [PrimaryKey]
        public int UserId { get; set; }

        [JsonProperty("AccessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("RefreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("CompanyId")]
        public int CompanyId { get; set; }
    }
}
