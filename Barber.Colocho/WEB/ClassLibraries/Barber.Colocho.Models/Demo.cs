using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Barber.Colocho.Models
{
    public class Demo
    {
        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("colony")]
        public string Colony { get; set; }

        [JsonProperty("municipality")]
        public string Municipality { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state_code")]
        public string StateCode { get; set; }

        [JsonProperty("zone_type")]
        public string ZoneType { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
