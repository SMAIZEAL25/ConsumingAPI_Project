using Newtonsoft.Json;

namespace ConsumingAPI_Project.Model
{
    public class CreateAPIObjectRequest
    {
        [JsonProperty ("Name")]
        public string Name { get; set; }

        [JsonProperty ("Data")]
        public Dictionary<string, Object> Data { get; set; } 
    }
}
