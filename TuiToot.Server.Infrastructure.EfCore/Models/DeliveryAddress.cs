using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuiToot.Server.Infrastructure.EfCore.Models
{
    public class DeliveryAddress
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public string DetailAddress { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
