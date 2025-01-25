using Microsoft.AspNetCore.Mvc;

namespace TuiToot.Server.Api.Dtos.Request
{
    public class ProductOrderCreationRequest
    {
        public string BagTypeId { get; set; }
        [FromForm]
        public IFormFile Image { get; set; }
    }
}
