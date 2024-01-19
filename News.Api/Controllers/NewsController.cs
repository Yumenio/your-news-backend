using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News.Api.Configuration;
using News.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace News.Api.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class NewsController : Controller
    {
        private readonly AppConfiguration _config;
        public NewsController(AppConfiguration config)
        {
            _config= config;
        }

        HttpClient client = new HttpClient();
        public Dictionary<int, string> categories = new Dictionary<int, string>()
        {
            {0, "arts"},
            {1, "automobiles"},
            {2, "books"},
            {3, "business"},
            {4, "fashion"},
            {5, "food"},
            {6, "health"},
            {7, "home"},
            {8, "insider"},
            {9, "magazine"},
            {10, "movies"},
            {11, "nyregion"},
            {12, "obituaries"},
            {13, "opinion"},
            {14, "politics"},
            {15, "realestate"},
            {16, "science"},
            {17, "sports"},
            {18, "sundayreview"},
            {19, "technology"},
            {20, "theater"},
            {21, "tmagazine"},
            {22, "travel"},
            {23, "upshot"},
            {24, "us"},
            {25, "world"}
        };

        [HttpGet]
        [Route("top-headlines")]
        public async Task<IActionResult> IndexTopHeadlines([Range(0,25)] [Required]int category)
        {
            string ApiKey =  _config.ApiKey ?? throw new ArgumentException("ApiKey not provided");
            string ApiUrl = _config.TopHeadlinesUrl ?? throw new ArgumentException("TopHeadlines URL not provided");
            try
            {

                string uri = ApiUrl + categories[category] + ".json";
                uri += $"?api-key={ApiKey}";
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    NewsDto newsDto = JsonSerializer.Deserialize<NewsDto>(responseBody);
                    string processedResponse = JsonSerializer.Serialize<NewsDto>(newsDto);
                    return Ok(processedResponse);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Error: {response.ReasonPhrase}");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
