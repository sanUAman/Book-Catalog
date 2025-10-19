using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesBook.Models;

namespace RazorPagesBook.Data
{
    public class RazorPagesBookContext : DbContext
    {
        public RazorPagesBookContext (DbContextOptions<RazorPagesBookContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesBook.Models.Book> Book { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<Feedback> Feedbacks { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Review>()
                .HasIndex(r => new { r.BookId, r.UserId })
                .IsUnique();
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
