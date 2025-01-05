using BookMangementSystemApi.Models;

namespace BookMangementSystemApi.Repository
{
    public interface IReaderRepository : IGenericRepository<Reader>
    {
        Task<Reader?> GetReaderByName(string name);
    }
}
