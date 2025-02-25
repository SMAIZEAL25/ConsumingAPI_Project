using Newtonsoft.Json;

namespace ConsumingAPI_Project.Model
{
    /// <summary>
    /// Represents the response received after deleting an API object.
    /// </summary>
    public class DeleteApiObjectResponse
    {
        /// <summary>
        /// Gets or sets the message indicating the result of the delete operation.
        /// </summary>
        [JsonProperty("message")]
        public string? Id { get; set; }
    }
}