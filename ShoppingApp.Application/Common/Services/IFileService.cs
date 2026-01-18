using Microsoft.AspNetCore.Http;

namespace ShoppingApp.Application.Common.Services
{
    public interface IFileService
    {
        Task<string?> UploadAsync(IFormFile file, string folder);
        bool Delete(string fileUrl);
    }

}
