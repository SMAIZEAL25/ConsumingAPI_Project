using ConsumingAPI_Project.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Text;
using static System.Net.WebRequestMethods;

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
        public async Task<List<APIObject>> GetAPIObjectsAsync()
        {
            List<APIObject> apiObjects = new List<APIObject>();

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}objects");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            apiObjects = JsonConvert.DeserializeObject<List<APIObject>>(result)!;

            return apiObjects;
        }



        // GET api/<HttpController>/5
        [HttpGet("{id}Get/requestById")]
        public async Task<ActionResult<APIObject?>> GetObjectIdAsync(string id)
        {
            // Use a single, shared HttpClient instance (injected or static)
            var httpClient = new HttpClient();

            try
            {
                // Construct the full URL
                string url = $"https://api.restful-api.dev/objects/{id}";

                // Send the GET request
                var response = await httpClient.GetAsync(url);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var apiObjects = JsonConvert.DeserializeObject<APIObject>(apiResponse);
                        return Ok(apiObjects);
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
        public async Task<APIObject?> CreatedObjectAsync(CreateAPIObjectRequest request)
        {
            APIObject? aPIObject = null;
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(request);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_baseUrl}/objects", content);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            aPIObject = JsonConvert.DeserializeObject<APIObject>(responseJson);
            return aPIObject;
        }


        // PUT api/<HttpController>/5
        [HttpPut("{id}")]
        public async Task<APIObject?> UpdateObjectAsync(string id, UpdateAPIObject updateAPIObject)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(updateAPIObject);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"{_baseUrl}/objects/{id}", content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<APIObject>(responseJson);
        }


        // Partially Update the record 
        [HttpPatch("{Id}")]
        public async Task<APIObject?> PartiallyUpdateObjectAync(string Id, PartialUpdateApiObjectRequest partialUpdateApi)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(partialUpdateApi);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync($"{_baseUrl}/objects/{Id}", content);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<APIObject>(responseJson);
        }


        // DELETE api/<HttpController>/5
        [HttpDelete("{id}")]
        public async Task<DeleteApiObjectResponse?> DeleteApiObjectAsync(string id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync($"{_baseUrl}/objects/{id}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DeleteApiObjectResponse>(json);
        }
    }
}