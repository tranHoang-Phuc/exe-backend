namespace TuiToot.Server.Api.Dtos.Request
{
    public class AvalibleProductCreation
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public int UnitsInStock { get; set; }
    }
}
