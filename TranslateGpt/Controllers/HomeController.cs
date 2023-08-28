using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TranslateGpt.Models;

namespace TranslateGpt.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        private readonly List<string> mostLanguages = new List<string>()
        {
            "English",
            "Mandarin Chinese",
            "Hindi",
            "Spanish",
            "French",
            "Modern Standard Arabic",
            "Bengali",
            "Russian",
            "Portuguese",
            "Urdu",
            "Indonesian",
            "German",
            "Japanese",
            "Nigerian Pidgin",
            "Marathi",
            "Telugu",
            "Turkish",
            "Tamil",
            "Yue Chinese",
            "Vietnamese",
            "Tagalog",
            "Wu Chinese",
            "Korean",
            "Iranian Persian (Farsi)",
            "Hausa",
            "Egyptian Spoken Arabic",
            "Swahili",
            "Javanese",
            "Italian",
            "Western Punjabi",
            "Kannada",
            "Gujarati",
            "Thai"

        };

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, HttpClient httpClient)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            ViewBag.Languages = new SelectList(mostLanguages);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetGPTResponse(string query, string selectedlanguage)
        {
            // get the OpenAPIKey from appsettings.json
            var openAPIKey = _configuration["OpenAI:ApiKey"];
            //set up the httpclient with open ai
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"{openAPIKey}");
            //define the reuqest pauload 

            // send the request

            // get response
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}