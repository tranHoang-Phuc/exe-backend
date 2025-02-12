namespace TuiToot.Server.Api.Dtos.Request
{
    public class DeliveryAddressRequest
    {
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public string Phone { get; set; }
        public string DetailAddress { get; set; }
    }
}
