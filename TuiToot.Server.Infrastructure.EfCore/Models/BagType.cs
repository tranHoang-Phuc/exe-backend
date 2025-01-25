using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuiToot.Server.Infrastructure.EfCore.Models
{
    public class BagType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int UnitsInStock { get; set; }
        public decimal Price { get; set; }

        public string? Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
