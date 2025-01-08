using BookMangementSystemApi.Models;

namespace BookMangementSystemApi.Repository
{
    public interface IAutherRepository : IGenericRepository<Auther>
    {
        Task<Auther> GetByName(string name);
    }
}
