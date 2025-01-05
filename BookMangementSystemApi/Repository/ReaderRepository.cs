using BookMangementSystemApi.Data;
using BookMangementSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMangementSystemApi.Repository
{
    public class ReaderRepository :GenericRepository<Reader> , IReaderRepository
    {
        public ReaderRepository(AppDbContext context) : base(context) { }

        public async Task<Reader?> GetReaderByName(string name)
        {
            return await  _dbSet.Where(r=>r.Name == name).FirstOrDefaultAsync();  
        }
    }
}
