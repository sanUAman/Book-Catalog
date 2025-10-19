using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RazorPagesBook.Models;
public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(60, MinimumLength = 1)]
    [Display(Name = "Назва")]
    public string? Title { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Дата релізу")]
    public DateTime ReleaseDate { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "Жанр")]
    public string? Genre { get; set; }

    [Range(1, 5000)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    [Display(Name = "Ціна")]
    public decimal Price { get; set; }

    [StringLength(5)]
    [Required]
    [Display(Name = "Рейтинг")]
    public string Rating { get; set; } = string.Empty;

    public string? PosterPath { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
