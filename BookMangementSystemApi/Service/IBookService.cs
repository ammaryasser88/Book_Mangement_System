using BookMangementSystemApi.Dtos.Request;
using BookMangementSystemApi.Dtos.Response;
using BookMangementSystemApi.Models;

namespace BookMangementSystemApi.Service
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponse>> GetAllBooks();
        Task<BookResponse> GetBookById(int id);
        Task<BookResponse> AddBook(BookRequest bookRequest);
        Task<BookResponse> UpdateBook(int id, BookRequest bookRequest);
        Task DeleteBook(int id);
        
    }
}
