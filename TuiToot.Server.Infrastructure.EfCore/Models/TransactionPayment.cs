using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuiToot.Server.Infrastructure.EfCore.Models
{
    public class TransactionPayment
    {
        public string Id { get; set; }
        public virtual Order Order  { get; set; }
        public string OrderId { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal ProductCost { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
