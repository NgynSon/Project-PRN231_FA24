using Diary_Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Diary_Client.Controllers
{
    public class CommentController : Controller
    {
        private readonly HttpClient _httpClient;

        public CommentController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(int postId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7132/api/Comment/post/{postId}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var comments = JsonConvert.DeserializeObject<List<Comment>>(content);
                return View(comments);
            }
            catch (HttpRequestException)
            {
                return View(new List<Comment>());
            }
        }

        public IActionResult Create(int postId)
        {
            ViewBag.PostId = postId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentContent,UserId,PostId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(comment);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7132/api/Comment", contentString);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), new { postId = comment.PostId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the comment.");
                }
            }
            return View(comment);
        }
    }
}
