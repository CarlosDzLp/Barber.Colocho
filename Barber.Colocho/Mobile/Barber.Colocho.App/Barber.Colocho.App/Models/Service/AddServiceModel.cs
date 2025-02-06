using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.Service
{
    public class AddServiceModel
    {
        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Duration")]
        public double Duration { get; set; }

        [JsonProperty("IsHomeService")]
        public bool IsHomeService { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Price")]
        public decimal Price { get; set; }
    }
}
