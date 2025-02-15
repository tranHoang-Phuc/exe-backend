using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuiToot.Server.Api.Cores;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Services.IServices;
using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromForm]OrderCreationRequest request)
        {
           
            var response = await _orderService.CreateOrder(request);
            var baseResponse = new BaseResponse<OrderCreationResponse>
            {
                Data = response
            };
            return CreatedAtAction(nameof(GetOrder),new {baseResponse.Data.Id}, baseResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(string id)
        {
            var response = await _orderService.GetOrder(id);
            var baseResponse = new BaseResponse<OrderResponse>
            {
                Data = response
            };
            return Ok(baseResponse);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _orderService.GetOrders();
            var baseResponse = new BaseResponse<List<OrderResponse>>
            {
                Data = response
            };
            return Ok(baseResponse);
        }

        [HttpPut("status")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody]UpdateOrderStatus request)
        {
            var response = await _orderService.UpdateOrderStatus(request.OrderId, request.Status);
            var baseResponse = new BaseResponse<bool>
            {
                Data = response
            };  
            return Ok(baseResponse);
        }

        [HttpPost("availableProduct")]
        public async Task<IActionResult> CreateAvailableProductOrder([FromBody]AvalibleProductOrderCreation request)
        {
            var response = await _orderService.CreateAvailableProductOrder(request);
            var baseResponse = new BaseResponse<OrderResponse>
            {
                Data = response
            };
            return Ok(baseResponse);
        }

        [HttpGet("admin/{orderId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetOrderAdmin(string orderId)
        {
            var response = await _orderService.GetAdminOrder(orderId);
            var baseResponse = new BaseResponse<AdminOrderResponse>
            {
                Data = response
            };
            return Ok(baseResponse);
        }
    }
}
