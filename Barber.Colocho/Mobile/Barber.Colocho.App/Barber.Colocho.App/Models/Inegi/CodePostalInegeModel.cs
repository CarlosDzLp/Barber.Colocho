using Barber.Colocho.App.Models.Base;
using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.Inegi
{
    public class CodePostalInegeModel
    {
        [JsonProperty("State")]
        public GenericModel State { get; set; }

        [JsonProperty("City")]
        public GenericModel City { get; set; }

        [JsonProperty("ListColony")]
        public List<GenericModel> ListColony { get; set; }
    }
}
