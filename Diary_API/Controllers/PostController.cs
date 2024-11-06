using Diary_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diary_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly DiaryContext _context;

        public PostController(DiaryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            return await _context.Posts.Include(x => x.User).Include(x => x.Interacts).Include(x => x.Comments).Where(x=>x.IsPublic==true).OrderByDescending(x=>x.Id).ToListAsync();
        }

        [HttpGet("LikeCount/{id}")]
        public async Task<ActionResult<int>> GetCountLike(int id)
        {
            int post = _context.Interacts.Where(x => x.PostId == id).Count();

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByUserId(int userId)
        {
            return await _context.Posts.Where(p => p.UserId == userId).Include(x => x.User).Include(x => x.Interacts).Include(x => x.Comments).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost(Post post)
        {
            post.Date = DateTime.Now;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPostsByUserId), new { userId = post.UserId }, post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, Post updatedPost)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }
            post.Date = DateTime.Now;
            post.PostContent = updatedPost.PostContent;

            _context.Update(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Comments)
                .ThenInclude(c => c.Replies)
                //.Include(p => p.Interacts)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            // xoa comment và reply cua Post
            foreach (var comment in post.Comments)
            {
                foreach (var reply in comment.Replies)
                {
                    _context.Replies.Remove(reply);
                }
                _context.Comments.Remove(comment);
            }

            // Xóa like cua post
            //foreach (var interact in post.Interacts)
            //{
            //    _context.Interacts.Remove(interact);
            //}

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}/public")]
        public async Task<IActionResult> SetPostPublic(int id, [FromQuery] bool isPublic)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            post.IsPublic = isPublic;
            _context.Update(post);
            _context.SaveChanges();
            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
