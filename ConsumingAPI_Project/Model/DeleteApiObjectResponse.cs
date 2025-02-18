using Newtonsoft.Json;

namespace ConsumingAPI_Project.Model
{
    public class DeleteApiObjectResponse
    {
        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
