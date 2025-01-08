using Microsoft.AspNetCore.Identity;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
using TuiToot.Server.Api.Exceptions;
using TuiToot.Server.Api.Services.IServices;
using TuiToot.Server.Infrastructure.EfCore.DataAccess;
using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;


        public AuthService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var users = await _unitOfWork.ApplicationUserRepository
                .FindAsync(u => u.UserName.ToLower() == request.Email.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(users.FirstOrDefault(), request.Password);
            if (users.FirstOrDefault() == null || !isValid)
            {
                throw new AppException(ErrorCode.Unauthorized);
            }
            var roles = await _userManager.GetRolesAsync(users.FirstOrDefault());
            var token = await _jwtTokenGenerator.GenerateToken(users.FirstOrDefault(), roles);
            var loginResponse = new LoginResponse
            {
                AccessToken = token,
                ExpiresIn = 300
            };
            return loginResponse;
        }

        public async Task<ApplicationUserResponse> Register(RegistrationRequest request)
        {
            var user = new ApplicationUser
            {
                Name = request.Name,
                Email = request.Email,
                UserName = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                NormalizedEmail = request.Email.ToUpper()
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new AppException(ErrorCode.UserExisted);
            }

            var userToReturn = (await _unitOfWork.ApplicationUserRepository
                .FindAsync(u => u.UserName == request.Email))
                .FirstOrDefault();       
            return new ApplicationUserResponse
            {
                Id = userToReturn!.Id,
                Name = userToReturn.Name,
                Email = userToReturn.Email,
                Address = userToReturn.Address,
                Phone = userToReturn.Phone
            };

        }
    }
}
