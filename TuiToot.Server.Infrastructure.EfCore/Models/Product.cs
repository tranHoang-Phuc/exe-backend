using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuiToot.Server.Infrastructure.EfCore.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string BagTypeId { get; set; }
        public virtual BagType BagType { get; set; }
        public string? Url { get; set; }
        public DateTime CreatedTime { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
