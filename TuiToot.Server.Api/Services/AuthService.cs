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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;


        public AuthService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<IntrospectResponse> IntrospectAsync(string token)
        {
            var response = new IntrospectResponse
            {
                IsValid = await _jwtTokenGenerator.IsTokenValidAsync(token)
            };
            return response;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
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
                ExpiresIn = 3600
            };
            return loginResponse;
        }

        public async Task<bool> LogoutAsync(string token)
        {
            await _unitOfWork.InvalidTokenRepository.AddAsync(new InvalidToken
            {
                Token = token
            });
      
            return await _unitOfWork.SaveChangesAsync() == 1;
        }

        public async Task<ApplicationUserResponse> RegisterAsync(RegistrationRequest request)
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
            await _userManager.AddToRoleAsync(user, "User");
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
