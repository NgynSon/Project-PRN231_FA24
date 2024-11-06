using System;
using System.Collections.Generic;

namespace Diary_Client.Models
{
    public partial class Reply
    {
        public int Id { get; set; }
        public string? ReplyContent { get; set; }
        public int? UserId { get; set; }
        public int? CommentId { get; set; }

        public virtual Comment? Comment { get; set; }
        public virtual User? User { get; set; }
    }
}
