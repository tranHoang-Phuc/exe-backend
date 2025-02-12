using Microsoft.AspNetCore.Mvc;
using TuiToot.Server.Api.Cores;
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
        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("<person><id>1</id><name>John Doe</name></person>");
        }
    }
}
