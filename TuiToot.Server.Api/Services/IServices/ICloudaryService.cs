using TuiToot.Server.Api.Dtos.Response;

namespace TuiToot.Server.Api.Services.IServices
{
    public interface ICloudaryService
    {
        Task<CloudaryUploadResponse> UploadImage(IFormFile file, string fileName, string folderName);
        Task<bool> DeleteImage(string publicId);
    }
}
