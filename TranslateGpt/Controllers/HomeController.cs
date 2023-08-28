using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;
using TranslateGpt.DTOs;
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
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAPIKey}");
            //define the reuqest pauload 
            var paylaod = new
            {
                model = "gpt-3.5-turbo",
                messages = new object[]
                {
                    new {role ="system", content=$"Translate the text to {selectedlanguage}"},
                    new {role = "user" , content=query}
                },
                temperature = 0,
                max_tokens = 256   
            };

            string jsonPayLoad = JsonConvert.SerializeObject(paylaod);   

            HttpContent httpContent = new StringContent(jsonPayLoad, Encoding.UTF8,"application/json");
            // send the request

            var responsemessage = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions",httpContent);

            var responseMessageJson  = await  responsemessage.Content.ReadAsStringAsync();

            // get response
            var response = JsonConvert.DeserializeObject<OpenAIResponse>(responseMessageJson);
            ViewBag.Result = response.Choices[0].Message.content;
            ViewBag.Languages = new SelectList(mostLanguages);
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}