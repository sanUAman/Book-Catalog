﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesBook.Data;
using RazorPagesBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPagesBook.Pages.Catalog
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly RazorPagesBookContext _context;
        public IndexModel(RazorPagesBookContext context) => _context = context;
        public IList<Book> Books { get; private set; } = new List<Book>();

        [Microsoft.AspNetCore.Mvc.BindProperty(SupportsGet = true)]
        public string? q { get; set; }

        [Microsoft.AspNetCore.Mvc.BindProperty(SupportsGet = true)]
        public string? genre { get; set; }

        public SelectList GenreOptions { get; private set; } = default!;

        public async Task OnGetAsync()
        {
            var genres = await _context.Book.AsNoTracking()
                .Where(m => !string.IsNullOrEmpty(m.Genre))
                .Select(m => m.Genre!)
                .Distinct()
                .OrderBy(g => g)
                .ToListAsync();

            GenreOptions = new SelectList(genres);

            var query = _context.Book.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(q))
            {
                var pattern = $"%{q}%";
                query = query.Where(m => EF.Functions.Like(m.Title, pattern));
            }
            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(m => m.Genre == genre);
            }
            Books = await query
                .OrderBy(m => m.Title)
                .ToListAsync();
        }
    }
}