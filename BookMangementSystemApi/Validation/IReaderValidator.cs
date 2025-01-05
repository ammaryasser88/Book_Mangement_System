namespace BookMangementSystemApi.Validation
{
    public interface IReaderValidator
    {
        Task ValidateReaderNameIsUnique(string readerName);
    }
}
