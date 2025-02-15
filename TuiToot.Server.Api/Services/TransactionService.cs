using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Exceptions;
using TuiToot.Server.Api.Services.IServices;
using TuiToot.Server.Infrastructure.EfCore.DataAccess;
using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly  IUnitOfWork _unitOfWork;
        private readonly  IHttpContextAccessor _httpContextAccessor;

        public TransactionService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<TransactionResponse>> GetAll()
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                throw new AppException(ErrorCode.Unauthorized);
            }
            var transactions = await _unitOfWork.TransactionRepository.GetAllAsync(
                t => t.Order.ApplicationUserId == userId,
                "Order"
            );
            var response = await transactions.Select(t => new TransactionResponse
            {
                Id = t.Id,               
                ShippingCost = t.ShippingCost,
                ProductCost = t.ProductCost,
                CreatedAt = t.CreatedAt,
                OrderId = t.Order.Id,
                Status = t.Order.OrderStatus
            }).OrderByDescending(t => t.CreatedAt).Take(100).ToListAsync();

            return response;
        }

        public async Task<TransactionResponse> GetById(string transactionId)
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                throw new AppException(ErrorCode.Unauthorized);
            }
            var response = await _unitOfWork.TransactionRepository.GetAsync(transactionId);

            if (response == null)
            {
                throw new AppException(ErrorCode.NotFound);
            }
            return new TransactionResponse
            {
                Id = response.Id,
                ShippingCost = response.ShippingCost,
                ProductCost = response.ProductCost,
                CreatedAt = response.CreatedAt,
                OrderId = response.OrderId,
            };
        }

        public async Task<TransactionResponse> GetByOrderId(string orderId)
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                throw new AppException(ErrorCode.Unauthorized);
            }
            var response = (await _unitOfWork.TransactionRepository
                .GetAllAsync(t => t.OrderId == orderId, "Order"))
                .FirstOrDefault();
            if (response == null) { 
                throw new AppException(ErrorCode.NotFound);
            }
            return new TransactionResponse
            {
                Id = response.Id,
                ShippingCost = response.ShippingCost,
                ProductCost = response.ProductCost,
                CreatedAt = response.CreatedAt,
                OrderId = response.OrderId,
            };
        }

        public async Task<List<TransactionResponse>> SearchById(string transactionId)
        {
            var transactions = await _unitOfWork.TransactionRepository.GetAllAsync(
                t => t.Id.Contains(transactionId), "Order");
            var response = transactions
                .Select(t => new TransactionResponse
            {
                Id = t.Id,
                ShippingCost = t.ShippingCost,
                ProductCost = t.ProductCost,
                CreatedAt = t.CreatedAt,
                OrderId = t.Order.Id,
                Status = t.Order.OrderStatus
            }).OrderByDescending(t => t.CreatedAt).Take(100);
            return await response.ToListAsync();
        }

        public async Task<IEnumerable<TransactionResponse>> SearchByOrderId(string orderId)
        {
            var orders = await _unitOfWork.OrderRepository
                .GetAllAsync(o => o.Id.Contains(orderId), "Transaction");
            var transactions = new List<TransactionResponse>();
            foreach (var order in orders)
            {
                var response = new TransactionResponse
                {
                    Id = order.Transaction.Id,
                    ShippingCost = order.Transaction.ShippingCost,
                    ProductCost = order.Transaction.ProductCost,
                    CreatedAt = order.Transaction.CreatedAt,
                    OrderId = order.Id,
                    Status = order.OrderStatus
                };
                transactions.Add(response);
            }
            return transactions;
        }

        private string GetUserIdFromToken()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
