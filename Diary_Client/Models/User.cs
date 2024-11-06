using System;
using System.Collections.Generic;

namespace Diary_Client.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Interacts = new HashSet<Interact>();
            Posts = new HashSet<Post>();
            Replies = new HashSet<Reply>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Name { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Dob { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Interact> Interacts { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
