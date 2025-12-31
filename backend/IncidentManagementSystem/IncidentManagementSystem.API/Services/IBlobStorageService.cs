using Microsoft.AspNetCore.Http;

namespace IncidentManagementSystem.API.Services
{
    public interface IBlobStorageService
    {
        Task<string> UploadAsync(IFormFile file, string fileName);
    }
}
