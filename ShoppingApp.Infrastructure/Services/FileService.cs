using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Application.Common.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        private string GetWebRootPath()
        {
            // Prefer configured WebRootPath
            if (!string.IsNullOrEmpty(_env.WebRootPath))
            {
                return _env.WebRootPath;
            }

            // Fallback to ContentRootPath/wwwroot
            var wwwroot = Path.Combine(_env.ContentRootPath, "wwwroot");
            if (!Directory.Exists(wwwroot))
            {
                Directory.CreateDirectory(wwwroot);
            }
            return wwwroot;
        }

        public async Task<string?> UploadAsync(IFormFile? file, string folder)
        {
            if (file == null || file.Length == 0)
                return null;

            if (string.IsNullOrWhiteSpace(folder))
                throw new ArgumentException("Folder cannot be empty.", nameof(folder));

            var webRoot = GetWebRootPath();
            var uploadPath = Path.Combine(webRoot, folder);
            Directory.CreateDirectory(uploadPath);

            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(extension) ||
                file.FileName.Equals("file", StringComparison.OrdinalIgnoreCase) ||
                file.FileName.Equals("blob", StringComparison.OrdinalIgnoreCase))
            {
                extension = file.ContentType switch
                {
                    "image/jpeg" or "image/jpg" => ".jpg",
                    "image/png" => ".png",
                    "image/gif" => ".gif",
                    "image/webp" => ".webp",
                    "image/svg+xml" => ".svg",
                    _ => ".jpg"
                };
            }

            var fileName = $"{Ulid.NewUlid()}{extension.ToLowerInvariant()}";
            var filePath = Path.Combine(uploadPath, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/{folder}/{fileName}";
        }
        public bool Delete(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
                return false;

            var webRoot = GetWebRootPath();
            var path = Path.Combine(webRoot, fileUrl.TrimStart('/'));

            if (!File.Exists(path))
                return false;

            File.Delete(path);
            return true;
        }
    }
}
