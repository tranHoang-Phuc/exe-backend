using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuiToot.Server.Infrastructure.EfCore.Models
{
    public enum OrderStatus
    {
        Pending,
        Deposited,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
