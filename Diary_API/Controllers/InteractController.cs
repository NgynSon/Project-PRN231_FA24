using Diary_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diary_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractController : ControllerBase
    {
        private readonly DiaryContext _context;

        public InteractController(DiaryContext context)
        {
            _context = context;
        }

        [HttpGet("post/{postId}")]
        public async Task<ActionResult<IEnumerable<Interact>>> GetInteractsByPostId(int postId)
        {
            return await _context.Interacts.Where(i => i.PostId == postId).Include(x=>x.User).ToListAsync();
        }

        [HttpPost("{userId}/{postId}")]
        public async Task<ActionResult<Interact>> AddInteract(int userId, int postId)
        {
            // Kiểm tra xem Interact đã tồn tại hay chưa
            var existingInteract = await _context.Interacts
                .FirstOrDefaultAsync(i => i.UserId == userId && i.PostId == postId);

            //if (existingInteract != null)
            //{
            //    return Conflict("Interact already exists.");
            //}
            var interact = new Interact
            {
                UserId = userId,
                PostId = postId,
                //LikeDate = DateTime.Now
            };
            _context.Interacts.Add(interact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInteractsByPostId), new { postId = interact.PostId }, interact);
        }

        [HttpDelete("{userId}/{postId}")]
        public async Task<IActionResult> DeleteInteract(int userId, int postId)
        {
            var interact = await _context.Interacts
                .FirstOrDefaultAsync(i => i.UserId == userId && i.PostId == postId);
            if (interact == null)
            {
                return NotFound();
            }

            _context.Interacts.Remove(interact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
