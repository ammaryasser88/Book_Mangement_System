using BookMangementSystemApi.Data;
using BookMangementSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMangementSystemApi.Repository.IMP
{
    public class AutherRepository : GenericRepository<Auther>, IAutherRepository
    {
        public AutherRepository(AppDbContext context) : base(context) { }

        public async Task<Auther> GetByName(string name)
        {
            return await _dbSet.Where(a => a.Name == name)
                .SingleOrDefaultAsync();

        }
    }
}
