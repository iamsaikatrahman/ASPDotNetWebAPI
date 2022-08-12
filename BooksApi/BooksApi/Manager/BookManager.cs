using BooksApi.Context;
using BooksApi.Interfaces.Manager;
using BooksApi.Interfaces.Repository;
using BooksApi.Model;
using BooksApi.Repository;
using EF.Core.Repository.Manager;

namespace BooksApi.Manager
{
    public class BookManager : CommonManager<Book>, IBookManager
    {
        public BookManager(BookDbContext _dbContext) : base(new BookRepository(_dbContext))
        {
        }

        public ICollection<Book> GetAll(string title)
        {
            return Get(c => c.Title.ToLower() == title.ToLower());
        }

        public ICollection<Book> GetBooks(int page, int pageSize)
        {
            if (page <= 1)
            {
                page = 0;
            }
            int totalNumber = page * pageSize;
            return GetAll().Skip(totalNumber).Take(pageSize).ToList();
        }

        public Book GetById(int id)
        {
            return GetFirstOrDefault(x => x.Id == id);
        }

        public ICollection<Book> SearchBook(string text)
        {
            //Single property search
            //return Get(c => c.Title.ToLower().Contains(text.ToLower()));
            //Multiple property search
            return Get(c => c.Title.ToLower().Contains(text.ToLower()) || c.Description.ToLower().Contains(text.ToLower()));
        }
    }
}
