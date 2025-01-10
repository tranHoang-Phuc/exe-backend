using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;

namespace TuiToot.Server.Api.Services.IServices
{
    public interface IOrderService
    {
        Task<OrderCreationResponse> CreateOrder(OrderCreationRequest orderRequest);

    }
}
