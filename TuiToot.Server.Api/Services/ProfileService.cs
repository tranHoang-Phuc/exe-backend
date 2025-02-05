using Microsoft.AspNetCore.Identity;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Exceptions;
using TuiToot.Server.Api.Services.IServices;
using TuiToot.Server.Infrastructure.EfCore.DataAccess;
using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ProfileResponse> GetUserProfile()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var response = new ProfileResponse
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.Name,
                PhoneNumber = user.Phone,
                Address = user.Address
            };
            return response;
        }

        public async Task<ProfileResponse> UpdateProfile(UpdatedProfileRequest profile, string userId)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            if (user.Id != userId || user.Id != profile.UserId || profile.UserId != userId)
            {
                throw new AppException(ErrorCode.ConflictData);
            }
            user.Name = profile.FullName;
            user.Phone = profile.PhoneNumber;
            user.Address = profile.Address;
            user.PhoneNumber = profile.PhoneNumber;
            await _userManager.UpdateAsync(user);
            var response = new ProfileResponse
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.Name,
                PhoneNumber = user.Phone,
                Address = user.Address
            };
            return response;
        }
    }
}
