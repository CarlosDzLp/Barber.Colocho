using Newtonsoft.Json;

namespace Barber.Colocho.App.Models.Base
{
    public class Response<T>
    {
        [JsonProperty("Result")]
        public T Result { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }
    }
}
