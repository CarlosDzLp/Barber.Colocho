using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.Authenticate
{
    public class RefreshTokenResponseModel
    {
        [JsonProperty("AccessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("RefreshToken")]
        public string RefreshToken { get; set; }
    }
}
