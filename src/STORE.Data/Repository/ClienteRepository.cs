using Microsoft.EntityFrameworkCore;
using STORE.Data.Constants;
using STORE.Data.Context;
using STORE.Entities.Commons;
using STORE.Entities.Dtos.Clientes;
using STORE.Entities.Interface;
using STORE.Entities.Models;

namespace STORE.Data.Repository
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        private readonly ApplicationDbContext _context;
        public ClienteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IEnumerable<ListClientesDto>>> ListClientesAsync()
        {
            var respose = new BaseResponse<IEnumerable<ListClientesDto>>();

            try
            {
                var clienteDetalle = await _context.ClientesArticulos
                 .Include(c => c.Cliente)
                 .Include(a => a.Articulo)
                 .GroupBy(ca => ca.Cliente)
                 .Select(group => new ListClientesDto
                 {
                     cliente = new ClienteDto
                     {
                         ClienteID = group.Key.ClienteID,
                         Nombre = group.Key.Nombre,
                         Apellidos = group.Key.Apellidos,
                         Direccion = group.Key.Direccion
                     },
                     ListClientesDetalle = group.Select(ca => new ListClientesDetalleDto
                     {
                         ClienteArticuloID = ca.ClienteArticuloID,
                         ClienteID = ca.Cliente.ClienteID,
                         ArticuloID = ca.Articulo.ArticuloID,
                         Codigo = ca.Articulo.Codigo,
                         Descripcion = ca.Articulo.Descripcion,
                         Precio = ca.Articulo.Precio,
                         Imagen = ca.Articulo.Imagen,
                         Stock = ca.Articulo.Stock,
                         Fecha = ca.Fecha
                     }).ToList()
                 })
                 .ToListAsync();

                if (clienteDetalle is not null)
                {
                    respose.IsSuccess = true;
                    respose.Data = clienteDetalle;
                    respose.Message = GlobalMessges.MESSAGE_QUERY;
                }

            }
            catch (Exception ex)
            {
                respose.Message = ex.Message;
            }

            return respose;
        }

        public async Task<BaseResponse<ListClientesDto>> ClienteByIdAsync(int ClienteArticuloID)
        {
            var respose = new BaseResponse<ListClientesDto>();

            try
            {
                var clienteArticulos = await _context.ClientesArticulos
                    .Include(c => c.Cliente)
                    .Include(a => a.Articulo)
                    .Where(c => c.ClienteArticuloID == ClienteArticuloID)
                    .GroupBy(ca => ca.Cliente)
                    .Select(group => new ListClientesDto
                    {
                        cliente = new ClienteDto
                        {
                            ClienteID = group.Key.ClienteID,
                            Nombre = group.Key.Nombre,
                            Apellidos = group.Key.Apellidos,
                            Direccion = group.Key.Direccion
                        },
                        ListClientesDetalle = group.Select(ca => new ListClientesDetalleDto
                        {
                            ClienteArticuloID = ca.ClienteArticuloID,
                            ClienteID = ca.Cliente.ClienteID,
                            ArticuloID = ca.Articulo.ArticuloID,
                            Codigo = ca.Articulo.Codigo,
                            Descripcion = ca.Articulo.Descripcion,
                            Precio = ca.Articulo.Precio,
                            Imagen = ca.Articulo.Imagen,
                            Stock = ca.Articulo.Stock,
                            Fecha = ca.Fecha
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (clienteArticulos is not null)
                {
                    respose.IsSuccess = true;
                    respose.Data = clienteArticulos;
                    respose.Message = GlobalMessges.MESSAGE_QUERY;
                }

            }
            catch (Exception ex)
            {
                respose.Message = ex.Message;
            }

            return respose;
        }

        public async Task<BaseResponse<bool>> ClienteCreateAsync(ClientesCreateDto clientesCreateDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var respose = new BaseResponse<bool>();

                try
                {
                    var clienteExist = await _context.Clientes
                        .Where(c => c.Nombre == clientesCreateDto.Nombre)
                        .Where(c => c.Apellidos == clientesCreateDto.Apellidos)
                        .FirstOrDefaultAsync();

                    if (clienteExist is not null)
                    {
                        respose.IsSuccess = false;
                        respose.Data = false;
                        respose.Message = GlobalMessges.MESSAGE_EXIST;
                        return respose;
                    }

                    var newCliente = new Cliente
                    {
                        Nombre = clientesCreateDto.Nombre,
                        Apellidos = clientesCreateDto.Apellidos,
                        Direccion = clientesCreateDto.Direccion,
                    };

                    await _context.Clientes.AddAsync(newCliente);
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

        public async Task<BaseResponse<bool>> ClienteCompraArticuloAsync(ClienteCompraArticuloDto clienteCompraArticuloDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var respose = new BaseResponse<bool>();

                try
                {
                    var clienteArticulo = new ClienteArticulo
                    {
                        ClienteID = clienteCompraArticuloDto.ClienteID,
                        ArticuloID = clienteCompraArticuloDto.ArticuloID,
                        Fecha = DateTime.Now
                    };

                    await _context.ClientesArticulos.AddAsync(clienteArticulo);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    respose.IsSuccess = true;
                    respose.Data = true;
                    respose.Message = GlobalMessges.MESSAGE_SAVE;

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

        public async Task<BaseResponse<bool>> ClienteEditAsync(ClientesCreateDto clientesCreateDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var respose = new BaseResponse<bool>();

                try
                {

                    var clienteExist = await _context.Clientes
                    .Where(c => c.ClienteID == clientesCreateDto.ClienteID)
                    .FirstOrDefaultAsync();

                    if (clienteExist is not null)
                    {

                        var clientExist = await _context.Clientes
                        .Where(c => c.Nombre == clientesCreateDto.Nombre)
                        .Where(c => c.Apellidos == clientesCreateDto.Apellidos)
                        .FirstOrDefaultAsync();

                        if (clientExist is not null)
                        {
                            respose.IsSuccess = false;
                            respose.Data = false;
                            respose.Message = GlobalMessges.MESSAGE_EXIST;
                            return respose;
                        }

                        clienteExist.Nombre = clientesCreateDto.Nombre;
                        clienteExist.Apellidos = clientesCreateDto.Apellidos;
                        clienteExist.Direccion = clientesCreateDto.Direccion;

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

        public async Task<BaseResponse<bool>> ClienteDeleteAsync(int ClienteID)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var respose = new BaseResponse<bool>();

                try
                {

                    var articuloExist = await _context.ClientesArticulos
                    .Include(a => a.Articulo)
                    .Include(c => c.Cliente)
                    .Where(c => c.ClienteID == ClienteID)
                    .FirstOrDefaultAsync();

                    if (articuloExist is not null)
                    {
                        _context.ClientesArticulos.Remove(articuloExist);
                        _context.Clientes.Remove(articuloExist.Cliente);
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
