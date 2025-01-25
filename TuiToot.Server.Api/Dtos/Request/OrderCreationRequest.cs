using Microsoft.AspNetCore.Mvc;

namespace TuiToot.Server.Api.Dtos.Request
{
    public class OrderCreationRequest
    {
        public string DeliveryAddressId { get; set; }
        [FromForm]
        public ICollection<ProductOrderCreationRequest> Products { get; set; }

    }
}
