using Microsoft.EntityFrameworkCore;
using RazorPagesBook.Data;
namespace RazorPagesBook.Models;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new RazorPagesBookContext(
        serviceProvider.GetRequiredService<
        DbContextOptions<RazorPagesBookContext>>()))
        {
            if (context == null || context.Book == null)
            {
                throw new ArgumentNullException("Null RazorPagesBookContext");
            }
            // Look for any movies.
            if (context.Book.Any())
            {
                return; // DB has been seeded
            }
            context.Book.AddRange(
            new Book
            {
                Title = "The Watchtower",
                ReleaseDate = DateTime.Parse("2020-03-15"),
                Genre = "Novel",
                Price = 7.72M
            },
            new Book
            {
                Title = "On the Edge",
                ReleaseDate = DateTime.Parse("2018-09-27"),
                Genre = "Drama",
                Price = 5.06M
            },
            new Book
            {
                Title = "Ghosts of the Past",
                ReleaseDate = DateTime.Parse("2022-07-03"),
                Genre = "Thriller",
                Price = 9.66M
            },
            new Book
            {
                Title = "Programming Basics",
                ReleaseDate = DateTime.Parse("2023-01-10"),
                Genre = "Educational",
                Price = 12.79M
            }
            );
            context.SaveChanges();
        }
    }
}