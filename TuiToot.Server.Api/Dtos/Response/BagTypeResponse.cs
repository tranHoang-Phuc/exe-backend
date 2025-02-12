namespace TuiToot.Server.Api.Dtos.Response
{
    public class BagTypeResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int UnitsInStock { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
