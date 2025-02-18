using Newtonsoft.Json;

namespace ConsumingAPI_Project.Model
{
    public class UpdateAPIObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }
    }
}
