using BookMangementSystemApi.Dtos.Request;
using BookMangementSystemApi.Dtos.Response;

namespace BookMangementSystemApi.Service
{
    public interface IReaderService
    {
        Task<ReaderResponse> AddReader(ReaderRequest readerRequest);
        Task<IEnumerable<ReaderResponse>> GetAllReaders();
        Task<ReaderResponse> GetReaderById(int id);
        Task<ReaderResponse> UpdateReader(int id, ReaderRequest readerRequest);
        Task DeleteReader(int id);
    }
}
