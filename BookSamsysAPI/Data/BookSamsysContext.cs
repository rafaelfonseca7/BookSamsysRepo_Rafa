using Microsoft.EntityFrameworkCore;
using BookSamsysAPI.Models;

namespace BookSamsysAPI.Data
{
    public class BookSamsysContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public BookSamsysContext(DbContextOptions<BookSamsysContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Book>()
                .HasQueryFilter(b => !b.IsDeleted);


            modelBuilder.Entity<Book>()
                .HasIndex(b => b.Isbn)
                .IsUnique();
        }
    }
}
