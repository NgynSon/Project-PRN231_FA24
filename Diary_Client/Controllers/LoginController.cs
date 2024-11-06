using Diary_Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Diary_Client.Controllers
{
	public class LoginController : Controller
	{
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Index(Login model)
		{
			var json = JsonConvert.SerializeObject(model);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			// Ignore SSL certificate errors (for development/testing only)
			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

			var client = new HttpClient(handler);
			var result = await client.PostAsync("https://localhost:7132/api/User/login", content);

			if (result.IsSuccessStatusCode)
			{
				var responseContext = await result.Content.ReadAsStringAsync();
				var token = JsonConvert.DeserializeObject<LoginResponse>(responseContext);

				HttpContext.Session.SetString("Token", token.Token);
				var handlerr = new JwtSecurityTokenHandler();
				var jsonToken = handlerr.ReadToken(token.Token) as JwtSecurityToken;
				var Username = jsonToken.Claims.First(claim => claim.Type == "Username").Value;
                var id = jsonToken.Claims.First(claim => claim.Type == "UserId").Value;
                var name = jsonToken.Claims.First(claim => claim.Type == "Username").Value;
                HttpContext.Session.SetString("Username", Username);
                HttpContext.Session.SetString("UserId", id);
                HttpContext.Session.SetString("Name", name);
                return RedirectToAction("Index", "Home");
			}

			HttpContext.Session.SetString("Token", await result.Content.ReadAsStringAsync());
			ViewBag.ErrorMessage = "Username or Password invalid.";
			return View();
		}

		[HttpGet]
        public IActionResult Logout()
        {
			HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Name");
            return RedirectToAction("Index", "Home");
        }
		
	}
}
