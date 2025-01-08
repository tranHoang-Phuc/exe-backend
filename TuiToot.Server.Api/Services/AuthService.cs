using Microsoft.AspNetCore.Identity;
using TuiToot.Server.Api.Dtos.Request;
using TuiToot.Server.Api.Dtos.Response;
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
            try
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    var userToReturn = (await _unitOfWork.ApplicationUserRepository
                        .FindAsync(u => u.UserName == request.Email))
                        .FirstOrDefault();

                    if (userToReturn == null)
                    {
                        throw new Exception();
                    }
                    return new ApplicationUserResponse
                    {
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        Email = userToReturn.Email,
                        Address = userToReturn.Address,
                        Phone = userToReturn.Phone
                    };
                }
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }
    }
}
