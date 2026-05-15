using System.ComponentModel.DataAnnotations;

namespace ReadingTracker.Models
{
    public class BookCreateViewModel
    {
        [Required(ErrorMessage = "Kitab adı daxil edin")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Müəllif adı daxil edin")]
        public string Author { get; set; }
    }
}