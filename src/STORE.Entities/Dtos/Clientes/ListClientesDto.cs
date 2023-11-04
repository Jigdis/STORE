namespace STORE.Entities.Dtos.Clientes
{
    public class ListClientesDto
    {
        public ClienteDto cliente { get; set; }
        public IEnumerable<ListClientesDetalleDto> ListClientesDetalle { get; set; }
    }
}
