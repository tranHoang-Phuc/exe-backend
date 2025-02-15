using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Services.IServices
{
    public interface IOrderService
    {
        Task<OrderCreationResponse> CreateOrder(OrderCreationRequest orderRequest);
        Task<OrderResponse> GetOrder(string id);
        Task<bool> UpdateOrderStatus(string id, OrderStatus status);
        Task<OrderResponse> CreateAvalibleProductOrder(AvalibleProductOrderCreation request);
        Task<List<OrderResponse>> GetOrders();
        Task<OrderResponse> CreateAvailableProductOrder(AvalibleProductOrderCreation request);
        Task<AdminOrderResponse> GetAdminOrder(string id);
    }
}
