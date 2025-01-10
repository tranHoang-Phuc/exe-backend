using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using TuiToot.Server.Api.Exceptions;
using TuiToot.Server.Api.Services.IServices;

namespace TuiToot.Server.Api.Services
{
    public class CloudaryService : ICloudaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }
        public async Task<string> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new AppException(ErrorCode.FileNotFound);
            }

            if (!IsValidImage(file))
            {
                throw new AppException(ErrorCode.InvalidFile);
            }
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(Guid.NewGuid().ToString(), stream),
                Transformation = new Transformation().Quality("auto").FetchFormat("auto")
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new AppException(ErrorCode.UploadFailed);
            }
            return uploadResult.SecureUrl.AbsoluteUri;
        }

        private bool IsValidImage(IFormFile file)
        {
            var validImageTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return validImageTypes.Contains(file.ContentType) && validExtensions.Contains(extension);
        }
    }
}
