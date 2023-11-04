using Microsoft.EntityFrameworkCore;
using STORE.Data.Constants;
using STORE.Data.Context;
using STORE.Entities.Commons;
using STORE.Entities.Dtos.Articulo;
using STORE.Entities.Interface;
using STORE.Entities.Models;

namespace STORE.Data.Repository
{
    public class ArticuloRepository : GenericRepository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticuloRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IEnumerable<ListArticulosDto>>> ListArticulosAsync()
        {
            var respose = new BaseResponse<IEnumerable<ListArticulosDto>>();

            try
            {
                var articulos = await _context.ArticulosTiendas
                    .Include(a => a.Articulo)
                    .Include(t => t.Tienda)
                    .Select(a => new ListArticulosDto
                    {
                        ArticuloTiendaID = a.ArticuloTiendaID,
                        ArticuloID = a.Articulo.ArticuloID,
                        Codigo = a.Articulo.Codigo,
                        Descripcion = a.Articulo.Descripcion,
                        Precio = a.Articulo.Precio,
                        Imagen = a.Articulo.Imagen,
                        Stock = a.Articulo.Stock,
                        TiendaID = a.Tienda.TiendaID,
                        Sucursal = a.Tienda.Sucursal,
                        Direccion = a.Tienda.Direccion,
                        Fecha = a.Fecha
                    })
                    .ToListAsync();

                if (articulos is not null)
                {
                    respose.IsSuccess = true;
                    respose.Data = articulos;
                    respose.Message = GlobalMessges.MESSAGE_QUERY;
                }

            }
            catch (Exception ex)
            {
                respose.Message = ex.Message;
            }

            return respose;
        }

        public async Task<BaseResponse<ListArticulosDto>> ArticuloByIdAsync(int ArticuloTiendaID)
        {
            var respose = new BaseResponse<ListArticulosDto>();

            try
            {
                var articulo = await _context.ArticulosTiendas
                    .Include(a => a.Articulo)
                    .Include(t => t.Tienda)
                    .Where(a => a.ArticuloTiendaID == ArticuloTiendaID)
                    .Select(a => new ListArticulosDto
                    {
                        ArticuloTiendaID = a.ArticuloTiendaID,
                        ArticuloID = a.Articulo.ArticuloID,
                        Codigo = a.Articulo.Codigo,
                        Descripcion = a.Articulo.Descripcion,
                        Precio = a.Articulo.Precio,
                        Imagen = a.Articulo.Imagen,
                        Stock = a.Articulo.Stock,
                        TiendaID = a.Tienda.TiendaID,
                        Sucursal = a.Tienda.Sucursal,
                        Direccion = a.Tienda.Direccion,
                        Fecha = a.Fecha
                    })
                    .FirstOrDefaultAsync();

                if (articulo is not null)
                {
                    respose.IsSuccess = true;
                    respose.Data = articulo;
                    respose.Message = GlobalMessges.MESSAGE_QUERY;
                }

            }
            catch (Exception ex)
            {
                respose.Message = ex.Message;
            }

            return respose;
        }

        public async Task<BaseResponse<bool>> ArticuloCreateAsync(ArticuloCreateDto articuloCreateDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var respose = new BaseResponse<bool>();

                try
                {
                    var articuloExist = await _context.Articulos
                        .Where(a => a.Codigo == articuloCreateDto.Codigo)
                        .FirstOrDefaultAsync();

                    if (articuloExist is not null)
                    {
                        respose.IsSuccess = false;
                        respose.Data = false;
                        respose.Message = GlobalMessges.MESSAGE_EXIST;
                        return respose;
                    }

                    var newArticulo = new Articulo
                    {
                        Codigo = articuloCreateDto.Codigo,
                        Descripcion = articuloCreateDto.Descripcion,
                        Imagen = articuloCreateDto.Imagen,
                        Precio = articuloCreateDto.Precio,
                        Stock = articuloCreateDto.Stock,
                    };

                    await _context.Articulos.AddAsync(newArticulo);
                    await _context.SaveChangesAsync();

                    var newArticuloTienda = new ArticuloTienda
                    {
                        ArticuloID = newArticulo.ArticuloID,
                        TiendaID = articuloCreateDto.TiendaID,
                        Fecha = DateTime.Now,
                    };

                    await _context.ArticulosTiendas.AddAsync(newArticuloTienda);
                    await _context.SaveChangesAsync();

                    respose.IsSuccess = true;
                    respose.Data = true;
                    respose.Message = GlobalMessges.MESSAGE_SAVE;
                    await transaction.CommitAsync();

                    return respose;

                }
                catch (Exception ex)
                {
                    respose.Message = ex.Message;
                    await transaction.RollbackAsync();
                    return respose;
                }
            }
        }

        public async Task<BaseResponse<bool>> ArticuloEditAsync(ArticuloCreateDto articuloCreateDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var respose = new BaseResponse<bool>();

                try
                {

                    var articuloExist = await _context.ArticulosTiendas
                    .Include(a => a.Articulo)
                    .Include(t => t.Tienda)
                    .Where(a => a.ArticuloID == articuloCreateDto.ArticuloID)
                    .FirstOrDefaultAsync();


                    if (articuloExist is not null)
                    {

                        var codeExist = await _context.Articulos
                        .Where(a => a.Codigo == articuloCreateDto.Codigo)
                        .FirstOrDefaultAsync();

                        if (codeExist is not null)
                        {
                            respose.IsSuccess = false;
                            respose.Data = false;
                            respose.Message = GlobalMessges.MESSAGE_EXIST;
                            return respose;
                        }

                        articuloExist.Articulo.Codigo = articuloCreateDto.Codigo;
                        articuloExist.Articulo.Descripcion = articuloCreateDto.Descripcion;
                        articuloExist.Articulo.Imagen = articuloCreateDto.Imagen;
                        articuloExist.Articulo.Precio = articuloCreateDto.Precio;
                        articuloExist.Articulo.Stock = articuloCreateDto.Stock;

                        articuloExist.TiendaID = articuloCreateDto.TiendaID;
                        articuloExist.Fecha = DateTime.Now;

                        await _context.SaveChangesAsync();

                        respose.IsSuccess = true;
                        respose.Data = true;
                        respose.Message = GlobalMessges.MESSAGE_SAVE;
                        await transaction.CommitAsync();

                    }

                    return respose;

                }
                catch (Exception ex)
                {
                    respose.Message = ex.Message;
                    await transaction.RollbackAsync();
                    return respose;
                }
            }
        }

        public async Task<BaseResponse<bool>> ArticuloDeleteAsync(int ArticuloID)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var respose = new BaseResponse<bool>();

                try
                {

                    var articuloExist = await _context.ArticulosTiendas
                    .Include(a => a.Articulo)
                    .Include(t => t.Tienda)
                    .Where(a => a.ArticuloID == ArticuloID)
                    .FirstOrDefaultAsync();

                    if (articuloExist is not null)
                    {
                        _context.ArticulosTiendas.Remove(articuloExist);
                        _context.Articulos.Remove(articuloExist.Articulo);
                        await _context.SaveChangesAsync();

                        respose.IsSuccess = true;
                        respose.Data = true;
                        respose.Message = GlobalMessges.MESSAGE_DELETE;
                        await transaction.CommitAsync();
                    }

                    return respose;

                }
                catch (Exception ex)
                {
                    respose.Message = ex.Message;
                    await transaction.RollbackAsync();
                    return respose;
                }
            }
        }
    }
}
