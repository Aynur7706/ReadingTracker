using System.ComponentModel.DataAnnotations;

namespace ReadingTracker.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad və soyad boş ola bilməz")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Username boş ola bilməz")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifrə boş ola bilməz")]
        public string Password { get; set; }
    }
}