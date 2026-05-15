using System.ComponentModel.DataAnnotations;

namespace ReadingTracker.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username daxil edin")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifrə daxil edin")]
        public string Password { get; set; }
    }
}