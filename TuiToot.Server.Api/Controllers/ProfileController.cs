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
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            var data = await _profileService.GetUserProfile();
            var response = new BaseResponse<ProfileResponse>()
            {
                Data = data
            };
            return Ok(response);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile([FromBody]UpdatedProfileRequest request, [FromRoute]string id)
        {
            var data = await _profileService.UpdateProfile(request, id);
            var response = new BaseResponse<ProfileResponse>()
            {
                Data = data
            };
            return Ok(response);
        }
    }
}
