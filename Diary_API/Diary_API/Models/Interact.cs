using System;
using System.Collections.Generic;

namespace Diary_API.Models
{
    public partial class Interact
    {
        public int? UserId { get; set; }
        public int? PostId { get; set; }
        public int Id { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
    }
}
