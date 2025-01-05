namespace BookMangementSystemApi.Service
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file);
        Task DeleteFile(string file);
    }
}
