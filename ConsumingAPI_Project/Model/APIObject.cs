using Newtonsoft.Json;

namespace ConsumingAPI_Project.Model
{
    /// <summary>
    /// Represents an API object with various properties.
    /// </summary>
    public class APIObject
    {
        /// <summary>
        /// Gets or sets the ID of the API object.
        /// </summary>
        [JsonProperty("Id")]
        public string Id { get; set; }

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

        /// <summary>
        /// Gets or sets the creation date of the API object.
        /// </summary>
        [JsonProperty("CreadedAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last update date of the API object.
        /// </summary>
        [JsonProperty("updateAt")]
        public DateTime UpdateAt { get; set; }
    }
}