using Barber.Colocho.App.Models.Base;
using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.Service
{
    public class ServiceModel
    {
        [JsonProperty("ListImage")]
        public List<GenericModel> ListImage { get; set; }

        [JsonProperty("Duration")]
        public double Duration { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Price")]
        public decimal Price { get; set; }

        [JsonProperty("IsHomeService")]
        public bool IsHomeService { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("CompanyId")]
        public int CompanyId { get; set; }

        [JsonProperty("Rating")]
        public double Rating { get; set; }
    }
}
