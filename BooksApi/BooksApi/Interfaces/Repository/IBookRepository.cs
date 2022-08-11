using BooksApi.Model;
using EF.Core.Repository.Interface.Repository;

namespace BooksApi.Interfaces.Repository
{
    public interface IBookRepository : ICommonRepository<Book>
    {
    }
}
