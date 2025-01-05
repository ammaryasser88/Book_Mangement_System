using BookMangementSystemApi.Exceptions;
using BookMangementSystemApi.Repository;

namespace BookMangementSystemApi.Validation
{
    public class ReaderValidator : IReaderValidator
    {
        IReaderRepository _readerRepository;

        public ReaderValidator(IReaderRepository readerRepository)
        {
            _readerRepository = readerRepository;
        }

        public async Task ValidateReaderNameIsUnique(string readerName)
        {
            var reader = await _readerRepository.GetReaderByName(readerName);
            if (reader is not null)
            {
                throw new ApiException("An Existing Reader Can Not Be Added.", 400);
            }
        }
    }
}
