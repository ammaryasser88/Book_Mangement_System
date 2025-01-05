using BookMangementSystemApi.Exceptions;
using BookMangementSystemApi.Repository;

namespace BookMangementSystemApi.Validation
{
    public class BookValidator : IBookValidator
    {
        IBookRepository _bookRepository;

        public BookValidator(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task ValidateBookTitleIsUnique(string text)
        {
            var book = await _bookRepository.GetBooksByTitle(text);
            if (book != null)
            {
                throw new ApiException("An Existing Book Can Not Be Added.", 400);
            }

        }
    }
}
