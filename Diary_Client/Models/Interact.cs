using System;
using System.Collections.Generic;

namespace Diary_Client.Models
{
    public class Interact
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime? LikeDate { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
