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
        

        public HttpController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _baseUrl = _configuration["baseurl"] ?? throw new ArgumentNullException("Base URL not configured");
            _httpClient = httpClient;
        }



        // GET: api/<HttpController>
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




        // GET api/<HttpController>/5
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



        // POST api/<HttpController>
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




        // PUT api/<HttpController>/5
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


        // Partially Update the record 
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


        // DELETE api/<HttpController>/5
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