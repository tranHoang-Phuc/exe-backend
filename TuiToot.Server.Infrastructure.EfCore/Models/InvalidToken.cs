using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuiToot.Server.Infrastructure.EfCore.Models
{
    public class InvalidToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
    }
}
