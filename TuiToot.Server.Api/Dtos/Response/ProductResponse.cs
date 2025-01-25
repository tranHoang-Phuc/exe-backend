namespace TuiToot.Server.Api.Dtos.Response
{
    public class ProductResponse
    {
        public string Id { get; set; }
        public string BagTypeId { get; set; }
        public string BagTypeName { get; set; }
        public decimal Price { get; set; }
        public string Url { get; set; }
    }
}
