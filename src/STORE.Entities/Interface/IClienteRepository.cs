using STORE.Entities.Commons;
using STORE.Entities.Dtos.Articulo;
using STORE.Entities.Dtos.Clientes;
using STORE.Entities.Models;

namespace STORE.Entities.Interface
{
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        Task<BaseResponse<IEnumerable<ListClientesDto>>> ListClientesAsync();
        Task<BaseResponse<ListClientesDto>> ClienteByIdAsync(int ClienteArticuloID);
        Task<BaseResponse<bool>> ClienteCreateAsync(ClientesCreateDto clientesCreateDto);
        Task<BaseResponse<bool>> ClienteCompraArticuloAsync(ClienteCompraArticuloDto clienteCompraArticuloDto);
        Task<BaseResponse<bool>> ClienteEditAsync(ClientesCreateDto clientesCreateDto);
        Task<BaseResponse<bool>> ClienteDeleteAsync(int ClienteID);
    }
}
