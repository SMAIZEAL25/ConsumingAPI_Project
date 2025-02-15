using ConsumingAPI_Project.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConsumingAPI_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public HttpController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        // GET: api/<HttpController>
        [HttpGet]
        [Route("Get/AllObject")]
        public async Task<List<APIObject>> GetAPIObjectsAsync()
        {
            List<APIObject> apiObjects = new List<APIObject>();

            var httpClient = new HttpClient();

               var response = await httpClient.GetAsync("https://restful-api.dev/Object");
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    apiObjects = JsonConvert.DeserializeObject<List<APIObject>>(apiResponse);
                }
            
            return apiObjects;
        }

        // GET api/<HttpController>/5
        [HttpGet("{id}Get/requestById")]

        public async Task<APIObject> GetObjectIdAsync(string Id)
        {
            APIObject aPIObject = new APIObject();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://api.restful-api.dev/objects?id=3&id=5&id=10" + Id);
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                aPIObject = JsonConvert.DeserializeObject<APIObject>(apiResponse);
            }
            return aPIObject;
        }


            // POST api/<HttpController>
        [HttpPost]
        public async Task<APIObject> CreatedObjectAsync (CreateAPIObjectRequest request)

        {
            APIObject aPIObject = new APIObject();
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(request);

            var content = new StringContent(json, Encoding.UTF8, "https://api.restful-api.dev/objects");

            var response = await httpClient.PostAsync("https://api.restful-api.dev/objects", content);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<APIObject>(responseJson);
        }


        // PUT api/<HttpController>/5
        [HttpPut("{id}")]
        public async Task<APIObject> UpdateObjectAsync(string id, UpdateAPIObject updateAPIObject)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(updateAPIObject);

            var content = new StringContent(json, Encoding.UTF8, "https://api.restful-api.dev/objects");

            var response = await httpClient.PutAsync($"https://api.restful-api.dev/objects/{id}", content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<APIObject>(responseJson);
        }


        // Partially Update the record 
        [HttpPatch("{Id}")]
        public async Task<APIObject> PartiallyUpdateObjectAync (string Id, PartialUpdateApiObjectRequest partialUpdateApi)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(partialUpdateApi);

            var content = new StringContent(json, Encoding.UTF8, "");

            var response = await httpClient.PatchAsync($"https://api.restful-api.dev/objects/{Id}", content);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<APIObject>(responseJson);
        }


        // DELETE api/<HttpController>/5
        [HttpDelete("{id}")]

        public async Task<DeleteApiObjectResponse> DeleteApiObjectAsync (string id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync($"https://api.restful-api.dev/objects/ {id}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DeleteApiObjectResponse>(json);
        }
    }
}
