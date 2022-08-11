using BooksApi.Model;
using EF.Core.Repository.Interface.Manager;

namespace BooksApi.Interfaces.Manager
{
    public interface IBookManager : ICommonManager<Book>
    {
        Book GetById(int id);

        ICollection<Book> GetAll(string title);
    }
}
