using System.Security.Claims;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Services.IServices;
using TuiToot.Server.Infrastructure.EfCore.DataAccess;
using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<OrderCreationResponse> CreateOrder(OrderCreationRequest orderRequest)
        {
            var userId = GetUserIdFromToken();
            // Upload ảnh lấy link
            var products = orderRequest.Products;

            // Tạo order

            var order = new Order
            {
                ApplicationUserId = userId,
                DeliveryAddressId = orderRequest.DeliveryAddressId,

            };

            // Tạo product order

            // Lưu vào db

            // Tạo bill cho order
        }

        private string GetUserIdFromToken()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Or "sub" if NameIdentifier is not set
        }
    }
}
