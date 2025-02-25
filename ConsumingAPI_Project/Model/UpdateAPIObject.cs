
using Newtonsoft.Json;

namespace ConsumingAPI_Project.Model
{
    /// <summary>
    /// Represents a request to update an existing API object.
    /// </summary>
    public class UpdateAPIObject
    {
        /// <summary>
        /// Gets or sets the name of the API object.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the API object.
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }
    }
}