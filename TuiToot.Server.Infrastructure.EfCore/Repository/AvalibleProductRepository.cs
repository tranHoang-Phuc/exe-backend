using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuiToot.Server.Infrastructure.EfCore.DataAccess;
using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Infrastructure.EfCore.Repository
{
    public class AvalibleProductRepository : GenericRepository<AvalibleProduct>, IAvaliblreProductRepository
    {
        public AvalibleProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
