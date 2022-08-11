using BooksApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Context
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
        public DbSet<Book> Books {get; set; }
    }
}
