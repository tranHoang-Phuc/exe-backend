using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Dtos.Response
{
    public class TransactionResponse
    {
        public string Id { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal ProductCost { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
