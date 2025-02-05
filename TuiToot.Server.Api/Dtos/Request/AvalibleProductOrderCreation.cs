namespace TuiToot.Server.Api.Dtos.Request
{
    public class AvalibleProductOrderCreation
    {
        public List<AvalibleProductOrder> ProductOrders { get; set; }
        public string DeliveryAddressId { get; set; }

    }
}
