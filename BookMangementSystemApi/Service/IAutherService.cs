using BookMangementSystemApi.Dtos.Request;
using BookMangementSystemApi.Dtos.Response;

namespace BookMangementSystemApi.Service
{
    public interface IAutherService
    {
        Task<IEnumerable<AutherResponse>> GetAllAuthers();
        Task<AutherResponse> AddAuther(AutherRequest autherRequest);
        Task<AutherResponse> GetAutherById (int  id);
        Task<AutherResponse> UpdateAuther(int id,AutherRequest autherRequest);
        Task DeleteAuther(int id);
    }
}
