using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TuiToot.Server.Infrastructure.EfCore.DataAccess;

namespace TuiToot.Server.Api.Policies
{
    public class OrderAccessHandler : AuthorizationHandler<OrderAccessRequirement>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderAccessHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OrderAccessRequirement requirement)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                context.Fail();
                return;
            }
            var userId = GetUserIdFromToken();
            if (userId ==  null)
            {
                context.Fail();
                return;
            }
            var orderId = _httpContextAccessor.HttpContext.Request.RouteValues["orderId"]!.ToString();
            var order = await _unitOfWork.OrderRepository.GetAllAsync(o => o.Id == orderId && o.ApplicationUserId == userId);
            if (order.Count() == 0)
            {
                context.Fail();
                return;
            }
            context.Succeed(requirement);
        }

        private string GetUserIdFromToken()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Or "sub" if NameIdentifier is not set
        }
    }
}
