using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STORE.Entities.Models
{
    [Table("Articulo")]
    public class Articulo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticuloID { get; set; }

        [StringLength(100)]
        public string Codigo { get; set; }

        [StringLength(250)]
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int Stock { get; set; }
        public List<ArticuloTienda> ArticulosTiendas { get; set; }
        public List<ClienteArticulo> ClientesArticulos { get; set; }
    }
}
