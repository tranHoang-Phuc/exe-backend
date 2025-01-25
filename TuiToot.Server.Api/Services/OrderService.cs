using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Web;
using TuiToot.Server.Api.Cores;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Exceptions;
using TuiToot.Server.Api.Services.IServices;
using TuiToot.Server.Infrastructure.EfCore.DataAccess;
using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICloudaryService _cloudaryService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public OrderService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,
            ICloudaryService cloudaryService, IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _cloudaryService = cloudaryService;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<OrderCreationResponse> CreateOrder(OrderCreationRequest orderRequest)
        {
            var shippingAddressList = await _unitOfWork.DeliveryAddressRepository
                .FindAsync(d => d.Id == orderRequest.DeliveryAddressId);
            var shippingAddress = shippingAddressList.FirstOrDefault();
            if (shippingAddress == null)
            {
                throw new AppException(ErrorCode.NotFound);
            }

            // get shipping cost
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("token", _configuration["GHNApi:Token"]);
            client.DefaultRequestHeaders.Add("shop_id", _configuration["GHNApi:ShopId"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var queryParams = new Dictionary<string, string>()
            {
                {"service_id", _configuration["GHNApi:ServiceId"] },
                {"insurance_value", _configuration["GHNApi:InsuranceValue"] },
                {"coupon", "" },
                {"to_district_id", shippingAddress.DistrictId.ToString() },
                {"from_district_id", _configuration["GHNApi:FromDistrictId"] },
                {"weight", _configuration["GHNApi:Weight"] },
                {"length", _configuration["GHNApi:Length"] },
                {"width", _configuration["GHNApi:Width"] },
                {"height",  _configuration["GHNApi:Height"] }
            };

            var url = QueryHelpers.AddQueryString(_configuration["GHNApi:FeeUrl"], queryParams);
            var response = await client.GetAsync(url);
            string jsonData = await response.Content.ReadAsStringAsync();

            var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<ShippingResponse>>(jsonData);

            var userId = GetUserIdFromToken();
            var products = orderRequest.Products;
            List<Product> productsOfOrder = new List<Product>();
            await _unitOfWork.BeginTransactionAsync();
            decimal totalCost = 0;
            foreach (var product in products)
            {
                
                var guid = Guid.NewGuid().ToString();
                var responseCloudary = await _cloudaryService.UploadImage(product.Image, guid, "Products");
                var bagType = await _unitOfWork.BagTypeRepository.FindAsync(b => b.Id == product.BagTypeId);
                totalCost += bagType.First().Price;
                var productOfOrder = new Product
                {
                    Id = guid,
                    BagTypeId = product.BagTypeId,
                    Url = responseCloudary.Url,
                    PublicdId = responseCloudary.PublicId,
                    BagType = bagType.First()
                };
                productsOfOrder.Add(productOfOrder);
            }

            var transaction = new TransactionPayment
            {
                Id = Guid.NewGuid().ToString(),
                ShippingCost = deserializedResponse!.Data!.Total,
                ProductCost = totalCost
            };

            var order = new Order
            {
                ApplicationUserId = userId,
                Products = productsOfOrder,
                DeliveryAddressId = orderRequest.DeliveryAddressId,
                TransactionId = transaction.Id,
                Transaction = transaction
            };


            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return new OrderCreationResponse
            {
                Id = order.Id,
                ApplicationUserId = order.ApplicationUserId,
                DeliveryAddressId = order.DeliveryAddressId,
                OrderStatus = order.OrderStatus,
                ShippingCost = order.Transaction.ShippingCost,
                Products = order.Products.AsQueryable().Include(p => p.BagType).Select(p => new ProductCreationResponse
                {
                    Id = p.Id,
                    BagTypeName = p.BagType.Name,
                    Price = p.BagType.Price,
                    ImageUrl = p.Url
                }).ToList()
            };
        }

        public async Task<OrderResponse> GetOrder(string id)
        {
            var orderList = await _unitOfWork.OrderRepository.GetAllAsync(o => o.Id == id, "DeliveryAddress,Products");
            var order = orderList.FirstOrDefault();
            if (order == null)
            {
                throw new AppException(ErrorCode.NotFound);
            }
            var listProduct = await _unitOfWork.ProductRepository.GetAllAsync(p => p.OrderId == id, "BagType");
            return new OrderResponse
            {
                Id = order.Id,
                ApplicationUserId = order.ApplicationUserId,
                DetailAddress = order.DeliveryAddress.DetailAddress,
                OrderStatus = order.OrderStatus,
                Products = listProduct.Select(p => new ProductResponse
                {
                    Id = p.Id,
                    BagTypeId = p.BagTypeId,
                    BagTypeName = p.BagType.Name,
                    Url = p.Url,
                    Price = p.BagType.Price
                }).ToList()
            };

        }

        public async Task<OrderResponse> UpdateOrderStatus(string id, OrderStatus status)
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync(o => o.Id == id);
            var order = orders.First();
            order.OrderStatus = status;
            await _unitOfWork.SaveChangesAsync();
            return new OrderResponse
            {
                Id = order.Id,
                ApplicationUserId = order.ApplicationUserId,
                DetailAddress = order.DeliveryAddress.DetailAddress,
                OrderStatus = order.OrderStatus,
                Products = order.Products.Select(p => new ProductResponse
                {
                    Id = p.Id,
                    BagTypeId = p.BagTypeId,
                    BagTypeName = p.BagType.Name,
                    Url = p.Url,
                    Price = p.BagType.Price
                }).ToList()
            };
        }

        private string GetUserIdFromToken()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Or "sub" if NameIdentifier is not set
        }
    }
}
