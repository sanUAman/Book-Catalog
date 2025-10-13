using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesBook.Data;
using RazorPagesBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorPagesBook.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesBook.Data.RazorPagesBookContext _context;

        public IndexModel(RazorPagesBook.Data.RazorPagesBookContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? BookGenre { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<string> genreQuery = from m in _context.Book
                                            orderby m.Genre
                                            select m.Genre;

            var books = from m in _context.Book 
                        select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                books = books.Where(s => !string.IsNullOrEmpty(s.Title) && s.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(BookGenre))
            {
                books = books.Where(x => x.Genre == BookGenre);
            }

            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());

            Book = await books.ToListAsync();
        }
    }
}
