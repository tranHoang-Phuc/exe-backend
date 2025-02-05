using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Api.Dtos.Request
{
    public class AvalibleProductOrder
    {
        public string ProductId { get; set; }
        public string BagTypeId  { get; set; }
        public int Quantity { get; set; }
    }
}
