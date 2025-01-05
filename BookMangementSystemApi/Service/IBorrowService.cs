using BookMangementSystemApi.Dtos.Request;
using BookMangementSystemApi.Dtos.Response;

namespace BookMangementSystemApi.Service
{
    public interface IBorrowService
    {
        Task<BorrowResponse> BorrowBook(BorrowRequest borrowRequest);
        Task ReturnedBook(ReturnedBookRequest returnedBook);
    }
}
