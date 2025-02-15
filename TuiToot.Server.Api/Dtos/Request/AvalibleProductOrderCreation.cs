namespace TuiToot.Server.Api.Dtos.Request
{
    public class AvalibleProductOrderCreation
    {
        public List<AProductOrder> ProductOrders { get; set; }
        public string DeliveryAddressId { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
