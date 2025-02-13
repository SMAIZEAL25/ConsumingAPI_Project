using Newtonsoft.Json;

namespace ConsumingAPI_Project.Model
{
    public class APIObject
    {
        [JsonProperty ("Id")]
        public string  id { get; set; }

        [JsonProperty ("Name")]
        public string Name { get; set; }

        [JsonProperty ("Data")]
        public Dictionary<string, object> Data { get; set; }

        [JsonProperty ("CreadedAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty ("updateAt")]
        public DateTime UpdateAt { get; set; }



    }
}
