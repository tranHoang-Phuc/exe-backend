using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Dtos.Request
{
    public class UpdateOrderStatus
    {
        public string OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
