using System;
using System.Collections.Generic;

namespace Diary_Client.Models
{
    public partial class Comment
    {
        public Comment()
        {
            Replies = new HashSet<Reply>();
        }

        public int Id { get; set; }
        public string? CommentContent { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
