using Microsoft.EntityFrameworkCore.Storage;
using STORE.Data.Context;
using STORE.Data.Repository;
using STORE.Entities.Interface;

namespace STORE.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IArticuloRepository Articulo { get; private set; }
        public IClienteRepository Cliente { get; private set; }
        public ITiendaRepository  Tienda { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Articulo = new ArticuloRepository(_context);
            Cliente = new ClienteRepository(_context);
            Tienda = new TiendaRepository(_context);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> CompletedAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
