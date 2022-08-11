using BooksApi.Interfaces.Repository;
using BooksApi.Model;
using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Repository
{
    public class BookRepository : CommonRepository<Book>, IBookRepository
    {
        public BookRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
