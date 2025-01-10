namespace TuiToot.Server.Api.Dtos.Request
{
    public class OrderCreationRequest
    {
        public string DeliveryAddressId { get; set; }
        public List<ProductOrderCreationRequest> Products { get; set; }

    }
}
