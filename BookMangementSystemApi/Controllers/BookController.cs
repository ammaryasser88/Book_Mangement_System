using BookMangementSystemApi.Dtos.Request;
using BookMangementSystemApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMangementSystemApi.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IBorrowService _borrowService;
        //Black Mamba
       
        public BookController(IBookService bookService , IBorrowService borrowService)
        {
            _bookService = bookService;
            _borrowService = borrowService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BookRequest bookRequest)
        {
            var book = await _bookService.AddBook(bookRequest);
            return Ok(book);
        }

        [HttpPost("Borrow")]
        public async Task<IActionResult> Borrow([FromBody] BorrowRequest borrowRequest)
        {
            var borrowBook = await _borrowService.BorrowBook(borrowRequest);
            return Ok(borrowBook);
        }

        [HttpPost("Return")]
        public async Task<IActionResult> Return([FromBody] ReturnedBookRequest returnedRequest)
        {
             await _borrowService.ReturnedBook(returnedRequest);
             return Ok("The Book Has Been Successfully Returned");
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll([FromQuery]string? title)
        {
            var books = await _bookService.GetAllBooks(title);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetBookById(id);
            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] BookRequest bookRequest)
        {
            var book = await _bookService.UpdateBook(id, bookRequest);
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await  _bookService.DeleteBook(id);
            return Ok("The Book Has Been Successfully Deleted");
        }
    }
}
