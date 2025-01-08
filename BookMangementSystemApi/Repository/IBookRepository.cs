using BookMangementSystemApi.Dtos.Response;
using BookMangementSystemApi.Models;
using BookMangementSystemApi.Repository.IMP;

namespace BookMangementSystemApi.Repository
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByTitleLike(string bookTitle);
        Task<Book> GetBookByTitle(string bookTitle);
        Task<IEnumerable<Book>> GetAllBooksWithAuthers();
        Task<Book> GetBookByIdWithAuther(int id);
        
        
    }
}
