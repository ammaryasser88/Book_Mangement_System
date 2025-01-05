using BookMangementSystemApi.Data;
using BookMangementSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMangementSystemApi.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext db) : base(db) { }

        public async Task<Book> GetBooksByTitle(string bookTitle)
        {
            var book = await _dbSet.Where(x => x.Title== bookTitle)
                .FirstOrDefaultAsync();
            
            return book;  
        }
    }
}
