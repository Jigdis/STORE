namespace STORE.Entities.Dtos.Clientes
{
    public class ListClientesDetalleDto
    {
        public int ClienteArticuloID { get; set; }
        public int ClienteID { get; set; }
        public int ArticuloID { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int Stock { get; set; }
        public DateTime Fecha { get; set; }
    }
}
