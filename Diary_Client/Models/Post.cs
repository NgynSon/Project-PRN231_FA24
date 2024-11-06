using System;
using System.Collections.Generic;

namespace Diary_Client.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Interacts = new HashSet<Interact>();
        }

        public int Id { get; set; }
        public string? PostContent { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsPublic { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Interact> Interacts { get; set; }
    }
}
