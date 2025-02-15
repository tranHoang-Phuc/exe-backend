using System.Threading.Tasks;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Exceptions;
using TuiToot.Server.Api.Services.IServices;
using TuiToot.Server.Infrastructure.EfCore.DataAccess;
using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Services
{
    public class BagTypeService : IBagTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudaryService _cloudaryService;
        public BagTypeService(IUnitOfWork unitOfWork, ICloudaryService cloudaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudaryService = cloudaryService;
        }

        public async Task<BagTypeResponse> Create(BagTypeCreation bagTypeCreation)
        {
            var bagType = new BagType
            {
                Id = Guid.NewGuid().ToString(),
                Name = bagTypeCreation.Name,
                Description = bagTypeCreation.Description,
                Price = bagTypeCreation.Price,
                UnitsInStock = bagTypeCreation.UnitsInStock
            };
            var result = await _cloudaryService.UploadImage(bagTypeCreation.Image, bagType.Id, "BagType");
            bagType.Url = result.Url;
            bagType.PublicId = result.PublicId;
            await _unitOfWork.BagTypeRepository.AddAsync(bagType);
            await _unitOfWork.SaveChangesAsync();
            return new BagTypeResponse
            {
                Id = bagType.Id,
                Name = bagType.Name,
                Url = bagType.Url,
                UnitsInStock = bagType.UnitsInStock,
                Price = bagType.Price,
                Description = bagType.Description
            };
        }

        public async Task<bool> Delete(string id)
        {
            var bagType = await _unitOfWork.BagTypeRepository.GetAsync(id);
            if (bagType == null)
            {
                throw new AppException(ErrorCode.NotFound);
            }
            await _cloudaryService.DeleteImage(bagType.PublicId);
            await _unitOfWork.BagTypeRepository.DeleteAsync(bagType);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<BagTypeResponse>> GetAll()
        {
            var bagTypes = await _unitOfWork.BagTypeRepository.GetAllAsync();
            return bagTypes.Select(b => new BagTypeResponse
            {
                Id = b.Id,
                Name = b.Name,
                Url = b.Url,
                UnitsInStock = b.UnitsInStock,
                Price = b.Price,
                Description = b.Description
            }).ToList();
        }
    }
}
