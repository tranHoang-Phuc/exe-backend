using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Services.IServices
{
    public interface IAuthService
    {
        Task<ApplicationUserResponse> Register(RegistrationRequest request);

    }
}
