using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.Company
{
    public class AddCompanyModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("AddressId")]
        public int AddressId { get; set; }

        [JsonProperty("RFC")]
        public string Rfc { get; set; }
    }
}
