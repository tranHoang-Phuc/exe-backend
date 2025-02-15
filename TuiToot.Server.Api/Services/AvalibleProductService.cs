using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System.Drawing;
using System.Net.Http;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Exceptions;
using TuiToot.Server.Api.Services.IServices;
using TuiToot.Server.Infrastructure.EfCore.DataAccess;
using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Services
{
    public class AvalibleProductService : IAvalibleProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudaryService _cloudaryService;
        private readonly IHttpClientFactory _httpClient;

        public AvalibleProductService(IUnitOfWork unitOfWork, ICloudaryService cloudaryService, IHttpClientFactory httpClient)
        {
            _unitOfWork = unitOfWork;
            _cloudaryService = cloudaryService;
            _httpClient = httpClient;
        }

        public async Task<AvalibleProductResponse> Create(AvalibleProductCreation avalibleProductCreation)
        {
            var avalibleProduct = new AvalibleProduct
            {
                Id = Guid.NewGuid().ToString(),
                Name = avalibleProductCreation.Name,
                Price = avalibleProductCreation.Price,
                UnitsInStock = avalibleProductCreation.UnitsInStock
            };
            var result = await _cloudaryService.UploadImage(avalibleProductCreation.Image, avalibleProduct.Id, "AvalibleProduct");
            avalibleProduct.ImageUrl = result.Url;
            avalibleProduct.PublicImageId = result.PublicId;
            avalibleProduct.PreviewUrl = result.Url;
            avalibleProduct.PublicPreviewId = result.PublicId;
            await _unitOfWork.AvaliblreProductRepository.AddAsync(avalibleProduct);
            await _unitOfWork.SaveChangesAsync();

            return new AvalibleProductResponse
            {
                Id = avalibleProduct.Id,
                Name = avalibleProduct.Name,
                Price = avalibleProduct.Price,
                ImageUrl = avalibleProduct.ImageUrl,
                PreviewUrl = avalibleProduct.PreviewUrl,
                UnitsInStock = avalibleProduct.UnitsInStock
            };
        }

        public async Task<bool> Delete(string id)
        {
            await _unitOfWork.AvaliblreProductRepository.DeleteAsync(Guid.Parse(id));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AvalibleProductResponse>> GetAll()
        {
            var avalibleProducts = await _unitOfWork.AvaliblreProductRepository.GetAllAsync();
            return avalibleProducts.Select(x => new AvalibleProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                PreviewUrl = x.PreviewUrl,
                UnitsInStock = x.UnitsInStock
            }).ToList();
        }

        public async Task<AvalibleProductResponse> GetById(string id)
        {
            var avalibleProduct = await _unitOfWork.AvaliblreProductRepository.GetAsync(id);
            return new AvalibleProductResponse
            {
                Id = avalibleProduct.Id,
                Name = avalibleProduct.Name,
                Price = avalibleProduct.Price,
                ImageUrl = avalibleProduct.ImageUrl,
                PreviewUrl = avalibleProduct.PreviewUrl,
                UnitsInStock = avalibleProduct.UnitsInStock
            };
        }

        public async Task<AvalibleProductResponse> Update(string id, UpdatedAvaliableProduct data)
        {
            var avalibleProduct = await _unitOfWork.AvaliblreProductRepository.GetAsync(id);
            if (id != data.Id)
            {
                throw new AppException(ErrorCode.ConflictData);
            }
            if (data.Name != null)
            {
                avalibleProduct.Name = data.Name;
            }
            if (data.Price != null)
            {
                avalibleProduct.Price = data.Price.Value;
            }
            if (data.UnitsInStock != null)
            {
                avalibleProduct.UnitsInStock = data.UnitsInStock.Value;
            }
            if (data.Image != null)
            {
                var result = await _cloudaryService.UploadImage(data.Image, id, "Products");
                avalibleProduct.ImageUrl = result.Url;
            }
            // Update PreviewUrl

            await _unitOfWork.AvaliblreProductRepository.UpdateAsync(avalibleProduct);
            await _unitOfWork.SaveChangesAsync();
            return new AvalibleProductResponse
            {
                Id = avalibleProduct.Id,
                Name = avalibleProduct.Name,
                Price = avalibleProduct.Price,
                ImageUrl = avalibleProduct.ImageUrl,
                PreviewUrl = avalibleProduct.PreviewUrl,
                UnitsInStock = avalibleProduct.UnitsInStock
            };
        }


        public async Task<IFormFile> OverlayImagesAsync(IFormFile overlayImage, string backgroundUrl, string fileName)
        {
            using var backgroundStream = await _httpClient.CreateClient().GetStreamAsync(backgroundUrl);
            using var skBackground = SKBitmap.Decode(backgroundStream);
            using var overlayStream = overlayImage.OpenReadStream();
            using var skOverlay = SKBitmap.Decode(overlayStream);
            if (skBackground == null || skOverlay == null)
            {
                throw new AppException(ErrorCode.InvalidFile);
            }
            using var combinedImage = new SKBitmap(skBackground.Width, skBackground.Height);
            using var canvas = new SKCanvas(combinedImage);
            canvas.DrawBitmap(skBackground, 0, 0);
            int xPosition = (skBackground.Width - skOverlay.Width) / 2;
            int offset = 180;
            int yPosition = (skBackground.Height - skOverlay.Height) / 2 + offset;
            if (yPosition + skOverlay.Height > skBackground.Height)
                yPosition = skBackground.Height - skOverlay.Height;
            canvas.DrawBitmap(skOverlay, new SKRect(xPosition, yPosition, xPosition + skOverlay.Width,
                yPosition + skOverlay.Height));
            using var image = SKImage.FromBitmap(combinedImage);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            var memoryStream = new MemoryStream();
            data.SaveTo(memoryStream);
            memoryStream.Position = 0;
            return new FormFile(memoryStream, 0, memoryStream.Length, "file", fileName + ".png")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };
        }

        public async Task<IEnumerable<AvalibleProductResponse>> GetByIds(string[] ids)
        {
            var avalibleProducts = await _unitOfWork.AvaliblreProductRepository.GetAllAsync(p => ids.Contains(p.Id));
            return await avalibleProducts.Select(x => new AvalibleProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                PreviewUrl = x.PreviewUrl,
                UnitsInStock = x.UnitsInStock
            }).ToListAsync();
        }
    }
}
