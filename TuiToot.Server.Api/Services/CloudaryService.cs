using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using TuiToot.Server.Api.Dtos.Response;
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

        public async Task<bool> DeleteImage(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletionParams);
            return result.Result == "ok";

        }

        public async Task<CloudaryUploadResponse> UploadImage(IFormFile file, string fileName, string folderName)
        {
            if (file == null || file.Length == 0)
            {
                throw new AppException(ErrorCode.FileNotFound);
            }

            if (!IsValidImage(file))
            {
                throw new AppException(ErrorCode.InvalidFile);
            }
            // Check image file size
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
                Transformation = new Transformation().Quality("auto").FetchFormat("auto"),
                Folder = folderName
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new AppException(ErrorCode.UploadFailed);
            }
            return new CloudaryUploadResponse
            {
                Url = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId
            };
        }

        private bool IsValidImage(IFormFile file)
        {
            var validImageTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return validImageTypes.Contains(file.ContentType) && validExtensions.Contains(extension);
        }
    }
}
