using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;

namespace TuiToot.Server.Api.Services.IServices
{
    public interface IDeliveryAddressService
    {
        Task<DeliveryAddressResponse> CreateAsync(DeliveryAddressRequest request);
        Task<IEnumerable<DeliveryAddressResponse>> GetAllAsync();
    }
}
