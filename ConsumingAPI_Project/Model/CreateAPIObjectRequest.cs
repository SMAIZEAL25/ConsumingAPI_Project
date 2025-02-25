using Newtonsoft.Json;

namespace ConsumingAPI_Project.Model
{
    /// <summary>
    /// Represents a request to create a new API object.
    /// </summary>
    public class CreateAPIObjectRequest
    {
        /// <summary>
        /// Gets or sets the name of the API object.
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the API object.
        /// </summary>
        [JsonProperty("Data")]
        public Dictionary<string, object> Data { get; set; }
    }
}