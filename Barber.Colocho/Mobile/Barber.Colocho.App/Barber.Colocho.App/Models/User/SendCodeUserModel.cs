using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.User
{
    public class SendCodeUserModel
    {
        [JsonProperty("Code")]
        public string Code { get; set; }
    }
}
