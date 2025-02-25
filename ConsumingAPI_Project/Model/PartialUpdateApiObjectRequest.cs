
using Newtonsoft.Json;

namespace ConsumingAPI_Project.Model
{
    /// <summary>
    /// Represents a request to partially update an API object.
    /// </summary>
    public class PartialUpdateApiObjectRequest
    {
        /// <summary>
        /// Gets or sets the name of the API object.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}