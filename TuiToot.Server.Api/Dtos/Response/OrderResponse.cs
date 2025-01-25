using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Dtos.Response
{
    public class OrderResponse
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string DetailAddress { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public IEnumerable<ProductResponse> Products { get; set; }
    }
}
