using BookMangementSystemApi.Data;
using BookMangementSystemApi.Dtos.Response;
using BookMangementSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMangementSystemApi.Repository.IMP
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext db) : base(db) { }

        public async Task<Book> GetBookByTitle(string? bookTitle)
        {
            var book = await _dbSet.Where(x => x.Title.Equals(bookTitle))
               .Include(b => b.Auther)
               .FirstOrDefaultAsync();

            return book;
        }

        public async Task<IEnumerable<Book>> GetBooksByTitleLike(string? bookTitle)
        {
            var books = await _dbSet.Where(x => x.Title.Contains(bookTitle))
                .Include(b => b.Auther)
                .ToListAsync();

            return books;
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithAuthers()
        {
            var books = await _context.Books.Include(b => b.Auther).ToListAsync();
            return books;
        }

        public async Task<Book> GetBookByIdWithAuther(int id)
        {
            var books = await _context.Books.Include(b => b.Auther).FirstOrDefaultAsync();
            return books;
        }
    }
}
