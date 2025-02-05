
namespace TuiToot.Server.Api.Dtos.Request
{
    public class UpdatedAvaliableProduct
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public IFormFile? Image { get; set; }
        public int? UnitsInStock { get; set; }
    }
}
