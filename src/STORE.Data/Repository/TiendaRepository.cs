using Microsoft.EntityFrameworkCore;
using STORE.Data.Context;
using STORE.Entities.Interface;
using STORE.Entities.Models;

namespace STORE.Data.Repository
{
    public class TiendaRepository : GenericRepository<Tienda>, ITiendaRepository
    {
        private readonly ApplicationDbContext _context;
        public TiendaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> update(Tienda tienda)
        {
            var tiendaExist = await _context.Tiendas
                .Where(t => t.TiendaID == tienda.TiendaID)
                .FirstOrDefaultAsync();

            if(tiendaExist is not null)
            {
                tiendaExist.Sucursal = tienda.Sucursal;
                tiendaExist.Direccion = tienda.Direccion;
                _context.Tiendas.Update(tiendaExist);
            }

            var isUpdate = await _context.SaveChangesAsync() > 0;
            return isUpdate;
        }

    }
}
