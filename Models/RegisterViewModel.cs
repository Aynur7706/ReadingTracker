using System.ComponentModel.DataAnnotations;

namespace ReadingTracker.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad və soyad daxil edin")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Username daxil edin")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifrə daxil edin")]
        [MinLength(4, ErrorMessage = "Şifrə minimum 4 simvol olmalıdır")]
        public string Password { get; set; }
    }
}