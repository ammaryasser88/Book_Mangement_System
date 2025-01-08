using AutoMapper;
using BookMangementSystemApi.Dtos.Request;
using BookMangementSystemApi.Dtos.Response;
using BookMangementSystemApi.Exceptions;
using BookMangementSystemApi.Models;
using BookMangementSystemApi.Repository;
using BookMangementSystemApi.Validation;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookMangementSystemApi.Service.IMP
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAutherRepository _autherRepository;
        private readonly IMapper _mapper;
        private readonly IBookValidator _bookValidate;
        private readonly IFileService _fileService;
       
        public BookService(IBookRepository repository, IMapper mapper, IBookValidator bookValidate, IFileService fileService, IAutherRepository autherRepository)
        {
            _bookRepository = repository;
            _mapper = mapper;
            _bookValidate = bookValidate;
            _fileService = fileService;
            _autherRepository = autherRepository;
        }

        public async Task<BookResponse> AddBook(BookRequest bookRequest)
        {
            await _bookValidate.ValidateBookTitleIsUnique(bookRequest.Title);

            if(bookRequest.Image is null)
            {
                throw new ApiException("Image Is Required.",(int) HttpStatusCode.BadRequest);
            }


            var auther = _autherRepository.GetByIdAsync(bookRequest.AutherID);

            string path = await _fileService.SaveFile(bookRequest.Image);
            var newBook = _mapper.Map<Book>(bookRequest);

            newBook.ImagePath = path;

            await _bookRepository.AddAsync(newBook);
            await _bookRepository.SaveAsync();

            var bookResponse =_mapper.Map<BookResponse>(newBook);
            bookResponse.Auther.Name = auther.Result.Name;
            return bookResponse;
        }

        public async Task<IEnumerable<BookResponse>> GetAllBooks(string? title)
        {
            IEnumerable<Book> booksWithAuthers;
            if (title is null)
            {
                booksWithAuthers = await _bookRepository.GetAllBooksWithAuthers();
            }
            else
            {
                booksWithAuthers = await _bookRepository.GetBooksByTitleLike(title);
            }
            return _mapper.Map<IEnumerable<BookResponse>>(booksWithAuthers);
        }

        public async Task<BookResponse> GetBookById(int id)
        {
            var book = await _bookRepository.GetBookByIdWithAuther(id);
            if (book is null)
            {
                throw new ApiException("Book Is Not Found", (int)HttpStatusCode.NotFound);
            }

            var bookResponse = _mapper.Map<BookResponse>(book);
            return bookResponse;
        }

        public async Task<BookResponse> UpdateBook(int id, BookRequest bookRequest)
        {
            var book = await _bookRepository.GetByIdAsync(id);
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
           

            await _bookRepository.UpdateAsync(bookUpdated);
            await _bookRepository.SaveAsync();

            await _fileService.DeleteFile(imagePath);

            return _mapper.Map<BookResponse>(bookUpdated);
        }

        public async Task DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if(book is null)
            {
                throw new ApiException("Book Is Not Found" , (int)HttpStatusCode.NotFound);
            }
            var imagePath = book.ImagePath;

            await _bookRepository.DeleteAsync(book);
            await _bookRepository.SaveAsync();

            await _fileService.DeleteFile(imagePath);
        }





    }
}
