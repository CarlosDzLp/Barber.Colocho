using Barber.Colocho.App.Models.Address;
using Barber.Colocho.App.Models.Base;
using Barber.Colocho.App.Models.Suscription;
using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.Company
{
    public class CompanyModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("RFC")]
        public string RFC { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("ListImage")]
        public List<GenericModel> ListImage { get; set; }

        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("Address")]
        public AddressModel Address { get; set; }

        [JsonProperty("Suscription")]
        public SuscriptionModel Suscription { get; set; }

        [JsonProperty("Rating")]
        public double Rating { get; set; }

        [JsonProperty("IsSuscription")]
        public bool IsSuscription {  get; set; }

        [JsonIgnore]
        public string AddressComplete { get; set; }

        [JsonIgnore]
        public string Search { get; set; }

        [JsonIgnore]
        public string Image { get; set; }
    }
}
