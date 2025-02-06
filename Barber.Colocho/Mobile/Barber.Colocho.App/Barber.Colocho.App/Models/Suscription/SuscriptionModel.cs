using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Colocho.App.Models.Suscription
{
    public class SuscriptionModel
    {
        [JsonProperty("Price")]
        public decimal Price { get; set; }

        [JsonProperty("FinishSuscription")]
        public DateTime FinishSuscription { get; set; }

        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("InitSuscription")]
        public DateTime InitSuscription { get; set; }

        [JsonProperty("PlanName")]
        public string PlanName { get; set; }
    }
}
