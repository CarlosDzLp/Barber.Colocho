using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.Base
{
    public class GenericModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
