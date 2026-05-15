using System.ComponentModel.DataAnnotations;

namespace ReadingTracker.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kitab adı boş ola bilməz")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Müəllif adı boş ola bilməz")]
        public string Author { get; set; }

        public BookStatus Status { get; set; } = BookStatus.Oxunur;

        public int AppUserId { get; set; }
    }
}