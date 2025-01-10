namespace TuiToot.Server.Api.Dtos.Request
{
    public class ProductOrderCreationRequest
    {
        public string BagTypeId { get; set; }
        public IFormFile Image { get; set; }
    }
}
