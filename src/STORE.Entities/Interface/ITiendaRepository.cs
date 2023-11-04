using STORE.Entities.Commons;
using STORE.Entities.Models;

namespace STORE.Entities.Interface
{
    public interface ITiendaRepository : IGenericRepository<Tienda>
    {
        Task<bool> update(Tienda tienda);
    }
}
