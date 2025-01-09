using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuiToot.Server.Api.Cores;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Services.IServices;

namespace TuiToot.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IDeliveryAddressService _deliveryAddressService;

        public AddressController(IDeliveryAddressService deliveryAddressService)
        {
            _deliveryAddressService = deliveryAddressService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddAddress([FromBody]DeliveryAddressRequest request)
        {
            var response = await _deliveryAddressService.CreateAsync(request);
            var baseResponse = new BaseResponse<DeliveryAddressResponse>
            {
                Data = response
            };
            return CreatedAtAction(nameof(AddAddress), baseResponse);
        }

        [HttpGet("delivery-address")]
        public async Task<IActionResult> GetDeliveryAddress()
        {
            var response = await _deliveryAddressService.GetAllAsync();
            var baseResponse = new BaseResponse<IEnumerable<DeliveryAddressResponse>>
            {
                Data = response
            };
            return Ok(response);
        }
    }
}
