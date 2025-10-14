using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesBook.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required, StringLength(450)]
        public string UserId { get; set; } = string.Empty;

        [StringLength(128)]
        public string? ReviewerName { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(2000)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public Book? Book { get; set; }
    }
}