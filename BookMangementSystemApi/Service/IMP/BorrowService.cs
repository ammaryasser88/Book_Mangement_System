using BookMangementSystemApi.Data;
using BookMangementSystemApi.Dtos.Request;
using BookMangementSystemApi.Dtos.Response;
using BookMangementSystemApi.Models;
using BookMangementSystemApi.Repository;
using Microsoft.EntityFrameworkCore;
using BookMangementSystemApi.Exceptions;
using System.Net;

namespace BookMangementSystemApi.Service.IMP
{
    public class BorrowService : IBorrowService
    {
        private readonly IGenericRepository<Borrow> _repository;
        private readonly IGenericRepository<Book> _bookRepository;

        public BorrowService(IGenericRepository<Borrow> repository, IGenericRepository<Book> bookRepository)
        {
            _repository = repository;
            _bookRepository = bookRepository;

        }

        public async Task<BorrowResponse> BorrowBook(BorrowRequest borrowRequest)
        {
            var book = await _bookRepository.GetByIdAsync(borrowRequest.BookId);
            if (book is null)
            {
                throw new ApiException("This Book Is Not Found", (int)HttpStatusCode.NotFound);
            }
            if (book.Status == "Borrowed")
            {
                throw new ApiException("This Book Is Borrowed", (int)HttpStatusCode.NotFound);
            }


            var borrow = new Borrow()
            {
                BookId = borrowRequest.BookId,
                ReaderId = borrowRequest.ReaderId,
                BorrowDate = DateTime.Now,
                ReturnedDate = DateTime.Now.AddDays(borrowRequest.BorrowDayes),
                Status = "Borrowed"
            };

            book.Status = borrow.Status;

            await _repository.AddAsync(borrow);
            await _repository.SaveAsync();

            var borrowResponse = new BorrowResponse()
            {
                Id = borrow.Id,
                BookId = borrow.BookId,
                ReaderId = borrow.ReaderId,
                BorrowDate = borrow.BorrowDate,
                ReturnedDate = borrow.ReturnedDate,
                Status = borrow.Status
            };
            return borrowResponse;

        }

        public async Task ReturnedBook(ReturnedBookRequest returnedBook)
        {
            var borrowBook = await _repository.GetByIdAsync(returnedBook.BorrowId);
            if (borrowBook.Status == "Returned")
            {
                throw new ApiException("This Book Is Returned", (int)HttpStatusCode.BadRequest);
            }

            borrowBook.AcuatalDate = DateTime.Now;
            borrowBook.Status = "Returned";

            var book = await _bookRepository.GetByIdAsync(borrowBook.BookId);
            book.Status = "Exist";
            await _repository.SaveAsync();
        }


    }
}
