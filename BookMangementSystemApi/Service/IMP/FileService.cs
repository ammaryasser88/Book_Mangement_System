using BookMangementSystemApi.Exceptions;
using System.Linq;
using System.Net;

namespace BookMangementSystemApi.Service.IMP
{
    public class FileService : IFileService
    {

        private readonly HashSet<string> _allowedExtenstion = new HashSet<string>() { ".png", ".jpg" };
        private readonly long _maxAllowedPosterSize = 5 * 1024 * 1024;

        public async Task DeleteFile(string image)
        {
            if (!string.IsNullOrEmpty(image))
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), image);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            if (file is null)
            {
                throw new ApiException("File is Null", (int)HttpStatusCode.BadRequest);
            }
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, file.FileName);

            var posterExtension = Path.GetExtension(filePath).ToLower();
            if (!_allowedExtenstion.Contains(posterExtension))
                throw new ApiException("Invalid file extension.", (int)HttpStatusCode.BadRequest);

            if (filePath.Length > _maxAllowedPosterSize)
                throw new ApiException("Max allowed size for poster is 5MB!", (int)HttpStatusCode.BadRequest);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return filePath;
        }


    }

}
