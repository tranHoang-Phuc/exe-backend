using System.Security.Claims;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Exceptions;
using TuiToot.Server.Api.Services.IServices;
using TuiToot.Server.Infrastructure.EfCore.DataAccess;
using TuiToot.Server.Infrastructure.EfCore.Models;
using TuiToot.Server.Infrastructure.EfCore.Repository;

namespace TuiToot.Server.Api.Services
{
    public class DeliveryAddressService : IDeliveryAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeliveryAddressService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DeliveryAddressResponse> CreateAsync(DeliveryAddressRequest request)
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                throw new AppException(ErrorCode.Unauthorized);
            }
            var deliveryAddress = new DeliveryAddress
            {
                ApplicationUserId = userId,
                ProvinceId = request.ProvinceId,
                DistrictId = request.DistrictId,
                WardId = request.WardId,
                Phone = request.Phone,
                DetailAddress = request.DetailAddress
            };
            await _unitOfWork.DeliveryAddressRepository.AddAsync(deliveryAddress);
            await _unitOfWork.SaveChangesAsync();
            return new DeliveryAddressResponse
            {
                Id = deliveryAddress.Id,
                ProvinceId = deliveryAddress.ProvinceId,
                DistrictId = deliveryAddress.DistrictId,
                WardId = deliveryAddress.WardId,
                Phone = deliveryAddress.Phone,
                DetailAddress = deliveryAddress.DetailAddress
            };
        }

        public async Task DeleteAddress(string id)
        {
            var address = await _unitOfWork.DeliveryAddressRepository.GetAllAsync(da => da.Id == id);
            await _unitOfWork.DeliveryAddressRepository.DeleteAsync(address.First());
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<IEnumerable<DeliveryAddressResponse>> GetAllAsync()
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                throw new AppException(ErrorCode.Unauthorized);
            }
            var deliveryAddresses = await _unitOfWork.DeliveryAddressRepository.FindAsync(da => da.ApplicationUserId == userId);
            
            var response = deliveryAddresses.Select(da => new DeliveryAddressResponse
            {
                Id = da.Id,
                ProvinceId = da.ProvinceId,
                DistrictId = da.DistrictId,
                WardId = da.WardId,
                Phone = da.Phone,
                DetailAddress = da.DetailAddress
            }).OrderBy(da => da.DetailAddress);
            return response;
        }

        private string GetUserIdFromToken()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Or "sub" if NameIdentifier is not set
        }
    }
}
