using System.ComponentModel.DataAnnotations;

namespace Diary_Client.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = "Email is required")]

        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be at least 6 and at max 20 characters long.")]
        public string Password { get; set; }
    }

    
}
