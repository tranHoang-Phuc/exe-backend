namespace TuiToot.Server.Api.Services.IServices
{
    public interface ICloudaryService
    {
        Task<string> UploadImage(IFormFile file);
    }
}
