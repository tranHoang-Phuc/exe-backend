using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;

namespace TuiToot.Server.Api.Services.IServices
{
    public interface IProfileService
    {
        Task<ProfileResponse> GetUserProfile();
        Task<ProfileResponse> UpdateProfile(UpdatedProfileRequest profile, string userId);
    }
}
