using STORE.Entities.Commons;
using STORE.Entities.Dtos.Articulo;
using STORE.Entities.Models;

namespace STORE.Entities.Interface
{
    public interface IArticuloRepository : IGenericRepository<Articulo>
    {
        Task<BaseResponse<IEnumerable<ListArticulosDto>>> ListArticulosAsync();
        Task<BaseResponse<ListArticulosDto>> ArticuloByIdAsync(int ArticuloTiendaID);
        Task<BaseResponse<bool>> ArticuloCreateAsync(ArticuloCreateDto articuloCreateDto);
        Task<BaseResponse<bool>> ArticuloEditAsync(ArticuloCreateDto articuloCreateDto);
        Task<BaseResponse<bool>> ArticuloDeleteAsync(int ArticuloID);
    }
}
