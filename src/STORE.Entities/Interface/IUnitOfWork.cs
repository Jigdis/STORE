using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STORE.Entities.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CompletedAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        IArticuloRepository Articulo { get; }
        IClienteRepository Cliente { get; }
        ITiendaRepository Tienda { get; }
    }
}
