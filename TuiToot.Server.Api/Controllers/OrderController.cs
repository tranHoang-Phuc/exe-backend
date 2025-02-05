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

        [HttpPost("status")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody]UpdateOrderStatus request)
        {
            var response = await _orderService.UpdateOrderStatus(request.OrderId, request.Status);
            var baseResponse = new BaseResponse<OrderResponse>
            {
                Data = response
            };
            return Ok(baseResponse);
        }

        [HttpPost("avalibleProduct")]
        public Task<IActionResult> CreateAvalibleProductOrder(AvalibleProductOrderCreation request)
        {
            return null;
        }
    }
}
