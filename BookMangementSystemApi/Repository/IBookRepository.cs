using BookMangementSystemApi.Models;

namespace BookMangementSystemApi.Repository
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book> GetBooksByTitle(string bookTitle);
    }
}
