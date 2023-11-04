using System.ComponentModel.DataAnnotations;

namespace STORE.Data.Dtos.Tienda
{
    public class TiendaDto
    {
        public int? TiendaID { get; set; }
        public string? Sucursal { get; set; }
        public string? Direccion { get; set; }
    }
}
