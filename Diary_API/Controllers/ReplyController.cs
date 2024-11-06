using Diary_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diary_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly DiaryContext _context;

        public ReplyController(DiaryContext context)
        {
            _context = context;
        }

        [HttpGet("comment/{commentId}")]
        public async Task<ActionResult<IEnumerable<Reply>>> GetRepliesByCommentId(int commentId)
        {
            return await _context.Replies.Where(r => r.CommentId == commentId).Include(x => x.User).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Reply>> AddReply(Reply reply)
        {
            _context.Replies.Add(reply);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRepliesByCommentId), new { commentId = reply.CommentId }, reply);
        }

    }
}
