using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(ApplicationUser user, IEnumerable<string> roles);
        Task<string> GenerateRefreshToken();
        Task<bool> IsTokenValidAsync(string token);
    }
}
