using ConsumingAPI_Project.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ConsumingAPI_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="httpClient">The HTTP client.</param>
        /// <exception cref="ArgumentNullException">Thrown when base URL is not configured.</exception>
        public HttpController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _baseUrl = _configuration["baseurl"] ?? throw new ArgumentNullException("Base URL not configured");
            _httpClient = httpClient;
        }

        /// <summary>
        /// Gets all API objects.
        /// </summary>
        /// <returns>A list of API objects.</returns>
        [HttpGet]
        [Route("Get/AllObject")]
        public async Task<IActionResult> GetAPIObjectsAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/objects");
                var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var apiObjects = JsonConvert.DeserializeObject<List<APIObject>>(result);

                return Ok(apiObjects); // Return 200 OK with the list of objects.
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the API object by ID.
        /// </summary>
        /// <param name="id">The ID of the API object.</param>
        /// <returns>The API object with the specified ID.</returns>
        [HttpGet("{id}Get/requestById")]
        public async Task<IActionResult> GetObjectIdAsync(string id)
        {
            try
            {
                // Construct the full URL
                string url = $"{_baseUrl}/objects/{id}";

                // Send the GET request
                var response = await _httpClient.GetAsync(url);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var apiObject = JsonConvert.DeserializeObject<APIObject>(apiResponse);
                        return Ok(apiObject);
                    }
                    catch (JsonException ex)
                    {
                        return StatusCode(500, $"Failed to deserialize the response: {ex.Message}");
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound($"Resource with id {id} not found");
                }
                else
                {
                    // Handle other non-success status codes
                    return StatusCode((int)response.StatusCode, $"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Request error: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new API object.
        /// </summary>
        /// <param name="request">The request containing the details of the API object to create.</param>
        /// <returns>The created API object.</returns>
        [HttpPost]
        public async Task<IActionResult> CreatedObjectAsync(CreateAPIObjectRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/objects", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var aPIObject = JsonConvert.DeserializeObject<APIObject>(responseJson);
                    return Ok(aPIObject);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Request error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing API object.
        /// </summary>
        /// <param name="id">The ID of the API object to update.</param>
        /// <param name="updateAPIObject">The updated details of the API object.</param>
        /// <returns>The updated API object.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateObjectAsync(string id, UpdateAPIObject updateAPIObject)
        {
            try
            {
                var json = JsonConvert.SerializeObject(updateAPIObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_baseUrl}/objects/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var updatedObject = JsonConvert.DeserializeObject<APIObject>(responseJson);
                    return Ok(updatedObject);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Request error: {ex.Message}");
            }
        }

        /// <summary>
        /// Partially updates an existing API object.
        /// </summary>
        /// <param name="Id">The ID of the API object to update.</param>
        /// <param name="partialUpdateApi">The partial update details of the API object.</param>
        /// <returns>The updated API object.</returns>
        [HttpPatch("{Id}")]
        public async Task<IActionResult> PartiallyUpdateObjectAync(string Id, PartialUpdateApiObjectRequest partialUpdateApi)
        {
            try
            {
                var json = JsonConvert.SerializeObject(partialUpdateApi);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PatchAsync($"{_baseUrl}/objects/{Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var updatedObject = JsonConvert.DeserializeObject<APIObject>(responseJson);
                    return Ok(updatedObject);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Request error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an API object by ID.
        /// </summary>
        /// <param name="id">The ID of the API object to delete.</param>
        /// <returns>The response indicating the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApiObjectAsync(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/objects/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var deleteResponse = JsonConvert.DeserializeObject<DeleteApiObjectResponse>(json);
                    return Ok(deleteResponse);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Request error: {ex.Message}");
            }
        }
    }
}