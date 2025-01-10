using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuiToot.Server.Infrastructure.EfCore.Repository;

namespace TuiToot.Server.Infrastructure.EfCore.DataAccess
{
    public interface IUnitOfWork
    {
        AppDbContext Context { get; }
        IApplicationUserRepository ApplicationUserRepository { get; }
        IInvalidTokenRepository InvalidTokenRepository { get; }
        IDeliveryAddressRepository DeliveryAddressRepository { get; }
        IOrderRepository OrderRepository { get; }
        IBagTypeRepository BagTypeRepository { get; }
        IProductRepository ProductRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task DisposeAsync();
    }
}
