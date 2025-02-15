using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Dtos.Response
{
    public class AdminOrderResponse
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string DetailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public IEnumerable<ProductResponse> Products { get; set; }
        public TransactionResponse Transaction { get; set; }
    }
}
