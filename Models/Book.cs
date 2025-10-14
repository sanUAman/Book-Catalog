using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RazorPagesBook.Models;
public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(60, MinimumLength = 1)]
    public string? Title { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Release Date")]
    public DateTime ReleaseDate { get; set; }

    [Required]
    [StringLength(30)]
    public string? Genre { get; set; }

    [Range(1, 5000)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [StringLength(5)]
    [Required]
    public string Rating { get; set; } = string.Empty;

    public string? PosterPath { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
