
using Diary_Client.ViewModels;
using Diary_Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Text;

namespace Diary_Client.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // Bypass SSL certificate validation
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            _httpClient = new HttpClient(handler);
        }
        

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7132/api/User");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(content);
                return View(users);
            }
            catch (HttpRequestException)
            {
                return View(new List<User>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string username, string password, string repassword, string name, DateTime? dob, bool? gender)
        {
            var checkUserResponse = await _httpClient.GetAsync($"https://localhost:7132/api/User");
            var content = await checkUserResponse.Content.ReadAsStringAsync();
            var existingUsers = JsonConvert.DeserializeObject<List<User>>(content);
            if (existingUsers.Any(u => u.Username == username))
            {
                ViewBag.ErrorMessage = "Username already exists.";
                return View();
            }

            if (password != repassword)
            {
                ViewBag.ErrorMessage = "Password and Re-Password do not match.";
                return View();
            }

            var user = new User
            {
                Username = username,
                Password = password,
                Name = name,
                Dob = dob,
                Gender = gender
            };

            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(user);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7132/api/User", contentString);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            return View();
        }
    }
}
