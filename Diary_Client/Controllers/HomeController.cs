using Diary_Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Diary_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
