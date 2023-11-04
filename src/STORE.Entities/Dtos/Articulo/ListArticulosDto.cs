namespace STORE.Entities.Dtos.Articulo
{
    public class ListArticulosDto
    {
        public int ArticuloTiendaID { get; set; }
        public int ArticuloID { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int Stock { get; set; }
        public int TiendaID { get; set; }
        public string Sucursal { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
