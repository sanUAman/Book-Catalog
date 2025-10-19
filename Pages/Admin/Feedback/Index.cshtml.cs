﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesBook.Data;
using RazorPagesBook.Models;
namespace RazorPagesBook.Pages.Admin.Feedback
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesBookContext _ctx;
        public IndexModel(RazorPagesBookContext ctx) => _ctx = ctx;
        public record RowVM(int Id, string Name, string Email, string Subject, string Message,
        DateTime CreatedAt, bool Replied, DateTime? RepliedAt);
        public IList<RowVM> Items { get; private set; } = new List<RowVM>();
        [BindProperty(SupportsGet = true)]
        public string? q { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool onlyUnreplied { get; set; }
        public async Task OnGetAsync()
        {
            var query = _ctx.Feedbacks.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                var like = $"%{q}%";
                query = query.Where(f =>
                EF.Functions.Like(f.Email, like) ||
                EF.Functions.Like(f.Name, like) ||
                EF.Functions.Like(f.Subject, like) ||
                EF.Functions.Like(f.Message, like));
            }
            if (onlyUnreplied)
                query = query.Where(f => f.AdminRepliedAt == null);
            Items = await query
            .OrderByDescending(f => f.CreatedAt)
            .Select(f => new RowVM(
            f.Id, f.Name, f.Email, f.Subject, f.Message,
            f.CreatedAt,
            f.AdminRepliedAt != null,
            f.AdminRepliedAt
            ))
            .ToListAsync();
        }
    }
}
