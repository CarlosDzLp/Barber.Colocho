using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.Address
{
    public class AddAdressModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("CodePostal")]
        public string CodePostal { get; set; }

        [JsonProperty("IdColony")]
        public int IdColony { get; set; }

        [JsonProperty("Observations")]
        public string Observations { get; set; }

        [JsonProperty("IsDefault")]
        public bool IsDefault { get; set; }

        [JsonProperty("NumExt")]
        public string NumExt { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }
    }
}
