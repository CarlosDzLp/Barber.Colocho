using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.Plan
{
    public class PlanModel
    {
        [JsonProperty("Price")]
        public decimal Price { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("IsFree")]
        public bool IsFree { get; set; }

        [JsonProperty("SKU")]
        public string SKU { get; set; }

        [JsonProperty("QuantityUser")]
        public int QuantityUser { get; set; }
    }
}
