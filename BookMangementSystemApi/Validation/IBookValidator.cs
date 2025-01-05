namespace BookMangementSystemApi.Validation
{
    public interface IBookValidator
    {
        Task ValidateBookTitleIsUnique(string bookTitle);
    }
}
