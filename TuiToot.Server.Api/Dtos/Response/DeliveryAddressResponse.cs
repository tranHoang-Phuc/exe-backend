namespace TuiToot.Server.Api.Dtos.Response
{
    public class DeliveryAddressResponse
    {
        public string Id { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public string DetailAddress { get; set; }
    }
}
