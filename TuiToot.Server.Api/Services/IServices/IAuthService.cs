using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Services.IServices
{
    public interface IAuthService
    {
        Task<ApplicationUserResponse> RegisterAsync(RegistrationRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<bool> LogoutAsync(string token);
        Task<IntrospectResponse> IntrospectAsync(string token);
    }
}
