using Microsoft.AspNetCore.Mvc;
using TuiToot.Server.Api.Cores;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Services.IServices;

namespace TuiToot.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            LoginResponse response = await _authService.LoginAsync(request);
            BaseResponse<LoginResponse> baseResponse = new BaseResponse<LoginResponse>
            {
                Data = response
            };
            return Ok(baseResponse);
        }
       
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequest request)
        {
            ApplicationUserResponse response = await _authService.RegisterAsync(request);
            BaseResponse<ApplicationUserResponse> baseResponse = new BaseResponse<ApplicationUserResponse>
            {
                Data = response
            };
            return CreatedAtAction(nameof(RegisterAsync), baseResponse);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync([FromBody] LogoutRequest request)
        {
            bool response = await _authService.LogoutAsync(request.Token);
            BaseResponse<bool> baseResponse = new BaseResponse<bool>
            {
                Data = response
            };
           
            return Ok(baseResponse);
        }

        [HttpPost("introspect")]
        public async Task<IActionResult> Introspect([FromBody] IntrospectRequest request)
        {
            IntrospectResponse response = await _authService.IntrospectAsync(request.Token);
            BaseResponse<IntrospectResponse> baseResponse = new BaseResponse<IntrospectResponse>
            {
                Data = response
            };
            return Ok(baseResponse);
        }

    }
}
