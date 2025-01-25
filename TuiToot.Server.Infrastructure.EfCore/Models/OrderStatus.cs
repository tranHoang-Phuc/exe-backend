using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuiToot.Server.Infrastructure.EfCore.Models
{
    public enum OrderStatus
    {
        NeedToPay,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
