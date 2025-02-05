using Microsoft.AspNetCore.Mvc;
using TuiToot.Server.Api.Cores;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Services.IServices;

namespace TuiToot.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class AvalibleProductController : ControllerBase
    {
        private readonly IAvalibleProductService _avalibleProductService;

        public AvalibleProductController(IAvalibleProductService avalibleProductService)
        {
            _avalibleProductService = avalibleProductService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _avalibleProductService.GetAll();
            BaseResponse<IEnumerable<AvalibleProductResponse>> baseResponse = new BaseResponse<IEnumerable<AvalibleProductResponse>>
            {
                Data = response
            };
            return Ok(baseResponse);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]string id)
        {
            var response = await _avalibleProductService.GetById(id);
            BaseResponse<AvalibleProductResponse> baseResponse = new BaseResponse<AvalibleProductResponse>
            {
                Data = response
            };
            return Ok(baseResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AvalibleProductCreation avalibleProductCreation)
        {
            var response = await _avalibleProductService.Create(avalibleProductCreation);
            BaseResponse<AvalibleProductResponse> baseResponse = new BaseResponse<AvalibleProductResponse>
            {
                Data = response
            };
            return Ok(baseResponse);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdatedAvaliableProduct data)
        {
            var response = await _avalibleProductService.Update(id, data);
            BaseResponse<AvalibleProductResponse> baseResponse = new BaseResponse<AvalibleProductResponse>
            {
                Data = response
            };
            return Ok(baseResponse);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _avalibleProductService.Delete(id);
            BaseResponse<bool> baseResponse = new BaseResponse<bool>
            {
                Data = response
            };
            return Ok(baseResponse);
        }
    }
}
