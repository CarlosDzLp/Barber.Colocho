using Barber.Colocho.App.ViewModels.Base;
using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.Address
{
    public class AddressModel : BindableModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("IsDefault")]
        public bool IsDefault { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("NumExt")]
        public string NumExt { get; set; }

        [JsonProperty("CodePostal")]
        public string CodePostal { get; set; }

        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("ColonyName")]
        public string ColonyName { get; set; }

        [JsonProperty("Observations")]
        public string Observations { get; set; }

        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("CityName")]
        public string CityName { get; set; }

        [JsonProperty("StateName")]
        public string StateName { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("IdColony")]
        public int IdColony { get; set; }

        [JsonIgnore]
        public string FontFamily { get; set; }
        [JsonIgnore]
        public string Search { get; set; }
    }
}
