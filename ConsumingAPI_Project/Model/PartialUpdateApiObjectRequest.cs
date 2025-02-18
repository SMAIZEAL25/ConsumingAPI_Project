using Newtonsoft.Json;

namespace ConsumingAPI_Project.Model
{
    public class PartialUpdateApiObjectRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
