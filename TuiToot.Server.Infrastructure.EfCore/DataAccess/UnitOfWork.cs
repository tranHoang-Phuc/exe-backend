using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuiToot.Server.Infrastructure.EfCore.Repository;

namespace TuiToot.Server.Infrastructure.EfCore.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction = null;
        private IApplicationUserRepository _applicationUserRepository;
        private IInvalidTokenRepository _invalidTokenRepository;
        private IDeliveryAddressRepository _deliveryAddressRepository;
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
        private IBagTypeRepository _bagTypeRepository;
        private ITransactionRepository _transactionRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public AppDbContext Context => _context;

        public IApplicationUserRepository ApplicationUserRepository => _applicationUserRepository ??= new ApplicationUserRepository(_context);

        public IInvalidTokenRepository InvalidTokenRepository => _invalidTokenRepository ??= new InvalidTokenRepository(_context);

        public IDeliveryAddressRepository DeliveryAddressRepository => _deliveryAddressRepository ??= new DeliveryAddressRepository(_context);

        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_context);

        public IBagTypeRepository BagTypeRepository => _bagTypeRepository ??= new BagTypeRepository(_context);

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

        public ITransactionRepository TransactionRepository => _transactionRepository ??= new TransactionRepository(_context);

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction?.CommitAsync();
        }

        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
