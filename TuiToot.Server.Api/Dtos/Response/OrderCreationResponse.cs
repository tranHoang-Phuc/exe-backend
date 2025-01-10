using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Dtos.Response
{
    public class OrderCreationResponse
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string DeliveryAddressId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<ProductCreationResponse> Products { get; set; }
    }
}
