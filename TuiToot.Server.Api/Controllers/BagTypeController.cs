using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuiToot.Server.Api.Cores;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Services;

namespace TuiToot.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BagTypeController : ControllerBase
    {
        private readonly IBagTypeService _bagTypeService;

        public BagTypeController(IBagTypeService bagTypeService)
        {
            _bagTypeService = bagTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _bagTypeService.GetAll();
            var baseResponse = new BaseResponse<IEnumerable<BagTypeResponse>>
            {
                Data = response
            };
            return Ok(baseResponse);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromForm] BagTypeCreation bagTypeCreation)
        {
            var response = await _bagTypeService.Create(bagTypeCreation);
            var baseResponse = new BaseResponse<BagTypeResponse>
            {
                Data = response
            };
            return Ok(baseResponse);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            var response = await _bagTypeService.Delete(id);
            var baseResponse = new BaseResponse<bool>
            {
                Data = response
            };
            return Ok(baseResponse);
        }
    }
}
