using BookMangementSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMangementSystemApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) :base(options) { }
        
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Borrow> Borrows { get; set; }


    }
}
