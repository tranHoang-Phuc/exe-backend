namespace TuiToot.Server.Api.Dtos.Response
{
    public class AvalibleProductResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string PreviewUrl { get; set; }
        public int UnitsInStock { get; set; }
    }
}
