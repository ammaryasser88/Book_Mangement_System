using AutoMapper;
using BookMangementSystemApi.Dtos.Request;
using BookMangementSystemApi.Dtos.Response;
using BookMangementSystemApi.Exceptions;
using BookMangementSystemApi.Models;
using BookMangementSystemApi.Repository;
using BookMangementSystemApi.Validation;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookMangementSystemApi.Service.IMP
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly IMapper _mapper;
        private readonly IBookValidator _bookValidate;
        private readonly IFileService _fileService;
        public BookService(IGenericRepository<Book> repository, IMapper mapper, IBookValidator bookValidate, IFileService fileService)
        {
            _repository = repository;
            _mapper = mapper;
            _bookValidate = bookValidate;
            _fileService = fileService;
        }

        public async Task<BookResponse> AddBook(BookRequest bookRequest)
        {
            await _bookValidate.ValidateBookTitleIsUnique(bookRequest.Title);

            if(bookRequest.Image is null)
            {
                throw new ApiException("Image Is Required.",(int) HttpStatusCode.BadRequest);
            }



            string path = await _fileService.SaveFile(bookRequest.Image);
            var newBook = _mapper.Map<Book>(bookRequest);


            newBook.ImagePath = path;

            await _repository.AddAsync(newBook);
            await _repository.SaveAsync();

            return _mapper.Map<BookResponse>(newBook);
        }

        public async Task<IEnumerable<BookResponse>> GetAllBooks()
        {
            var books = await _repository.GetAllAsync();
           
            return _mapper.Map<IEnumerable<BookResponse>>(books);

        }

        public async Task<BookResponse> GetBookById(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book is null)
            {
                throw new ApiException("Book Is Not Found", (int)HttpStatusCode.NotFound);
            }
            return _mapper.Map<BookResponse>(book);
        }

        public async Task<BookResponse> UpdateBook(int id, BookRequest bookRequest)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book is null)
            {
                throw new ApiException("Book Is Not Found", (int)HttpStatusCode.NotFound);
            }
            string imagePath = book.ImagePath;

            var bookUpdated = _mapper.Map(bookRequest, book);
            if (bookRequest.Image!=null)
            {
                string path = await _fileService.SaveFile(bookRequest.Image);
                 bookUpdated.ImagePath = path;
            }
           

            await _repository.Update(bookUpdated);
            await _repository.SaveAsync();

            await _fileService.DeleteFile(imagePath);

            return _mapper.Map<BookResponse>(bookUpdated);
        }

        public async Task DeleteBook(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if(book is null)
            {
                throw new ApiException("Book Is Not Found" , (int)HttpStatusCode.NotFound);
            }
            var imagePath = book.ImagePath;

            await _repository.Delete(book);
            await _repository.SaveAsync();

            await _fileService.DeleteFile(imagePath);
        }





    }
}
