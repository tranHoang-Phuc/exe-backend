using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuiToot.Server.Infrastructure.EfCore.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string DeliveryAddressId { get; set; }
        public virtual DeliveryAddress DeliveryAddress { get; set; }
        public string TransactionId { get; set; }
        public virtual TransactionPayment Transaction { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.NeedToPay;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<Product> Products { get; set; }

    }
}
